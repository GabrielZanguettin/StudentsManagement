using StudentsManagement.Appliers.Students;
using StudentsManagement.Dtos.Students;
using StudentsManagement.Entities;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Common;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Interfaces.IServices;
using StudentsManagement.Mappings.Students;
using StudentsManagement.Validations.Common;
using StudentsManagement.Validations.Domain.Students;

namespace StudentsManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentService(
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            this._studentRepository = studentRepository;
            this._courseRepository = courseRepository;
        }

        public async Task<List<StudentResponseDto>> GetAll()
        {
            var students = await this._studentRepository.GetAll();

            return students
                .Select(s => s.ToStudentResponseDto())
                .ToList();
        }
            
        public async Task<StudentResponseDto> GetById(Guid id)
        {
            var student = await this._studentRepository.GetById(id);
            if (student is null)
                throw NotFoundException.For(EntityName.Student, id);

            return student.ToStudentResponseDto();
        }

        public async Task<StudentResponseDto> Create(CreateStudentDto dto)
        {
            var missing = RequiredFieldsValidator.GetMissing(
                (nameof(dto.FirstName), string.IsNullOrWhiteSpace(dto.FirstName)),
                (nameof(dto.Email), string.IsNullOrWhiteSpace(dto.Email)),
                (nameof(dto.CourseId), dto.CourseId == Guid.Empty)
            );

            if (missing.Length > 0)
                throw BadRequestException.RequiredFields(missing);

            var firstName = dto.FirstName.Trim();
            var email = dto.Email.Trim();
            var courseId = dto.CourseId;

            await StudentValidator.ValidateCreateAsync(
                firstName,
                email,
                courseId,
                this._courseRepository,
                this._studentRepository
            );

            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                Email = email,
                CourseId = courseId
            };

            var created = await this._studentRepository.Create(student);

            return await this.GetById(created.Id);
        }

        public async Task<StudentResponseDto> Update(Guid id, UpdateStudentDto dto)
        {
            var student = await this._studentRepository.GetById(id);
            if (student is null)
                throw NotFoundException.For(EntityName.Student, id);

            PatchValidator.EnsureHasAny(dto.FirstName, dto.Email, dto.CourseId);
            PatchValidator.EnsureNotBlank(nameof(dto.FirstName), dto.FirstName);
            PatchValidator.EnsureNotBlank(nameof(dto.Email), dto.Email);
            PatchValidator.EnsureNotEmptyGuid(nameof(dto.CourseId), dto.CourseId);

            await StudentValidator.ValidateUpdateAsync(
                dto.FirstName,
                dto.Email,
                dto.CourseId,
                this._courseRepository,
                this._studentRepository
            );

            StudentPatchApplier.ApplyStudentPatch(student, dto);

            var updatedStudent = await this._studentRepository.Update(student);

            return await this.GetById(updatedStudent.Id);
        }

        public async Task<StudentResponseDto> Delete(Guid id)
        {
            var student = await this._studentRepository.GetById(id);
            if (student is null)
                throw NotFoundException.For(EntityName.Student, id);

            await this._studentRepository.Delete(student);
            return student.ToStudentResponseDto();
        }
    }
}