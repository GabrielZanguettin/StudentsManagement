using StudentsManagement.Appliers.Institutions;
using StudentsManagement.Dtos.Institutions;
using StudentsManagement.Entities;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Common;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Interfaces.IServices;
using StudentsManagement.Mappings.Institutions;
using StudentsManagement.Validations.Common;
using StudentsManagement.Validations.Domain.Institutions;

namespace StudentsManagement.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionService(IInstitutionRepository institutionRepository)
        {
            this._institutionRepository = institutionRepository;
        }

        public async Task<List<InstitutionResponseDto>> GetAll()
        {
            var institutions = await this._institutionRepository.GetAll();

            return institutions
                .Select(i => i.ToInstitutionResponseDto())
                .ToList();
        }

        public async Task<InstitutionResponseDto> GetById(Guid id)
        {
            var institution = await this._institutionRepository.GetById(id);
            if (institution is null)
                throw NotFoundException.For(EntityName.Institution, id);

            return institution.ToInstitutionResponseDto();
        }

        public async Task<InstitutionResponseDto> Create(CreateInstitutionDto dto)
        {
            var missing = RequiredFieldsValidator.GetMissing(
                (nameof(dto.Name), string.IsNullOrWhiteSpace(dto.Name))
            );

            if (missing.Length > 0)
                throw BadRequestException.RequiredFields(missing);

            var name = dto.Name.Trim();

            InstitutionValidator.ValidateCreate(name);

            var institution = new Institution
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            var created = await this._institutionRepository.Create(institution);

            return await this.GetById(created.Id);
        }

        public async Task<InstitutionResponseDto> Update(Guid id, UpdateInstitutionDto dto)
        {
            var institution = await this._institutionRepository.GetById(id);
            if (institution is null)
                throw NotFoundException.For(EntityName.Institution, id);

            PatchValidator.EnsureHasAny(dto.Name);
            PatchValidator.EnsureNotBlank(nameof(dto.Name), dto.Name);

            InstitutionValidator.ValidateUpdate(dto.Name);
            InstitutionPatchApplier.ApplyInstitutionPatch(institution, dto);

            var updatedInstitution = await this._institutionRepository.Update(institution);

            return await this.GetById(updatedInstitution.Id);
        }

        public async Task<InstitutionResponseDto> Delete(Guid id)
        {
            var institution = await this._institutionRepository.GetById(id);
            if (institution is null)
                throw NotFoundException.For(EntityName.Institution, id);

            await this._institutionRepository.Delete(institution);

            return institution.ToInstitutionResponseDto();
        }
    }
}
