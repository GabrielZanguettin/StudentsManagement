using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Dtos.Institutions;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Api;
using StudentsManagement.Interfaces.IServices;

namespace StudentsManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;

        public InstitutionController(IInstitutionService institutionService)
        {
            this._institutionService = institutionService;
        }

        [HttpGet("institutions")]
        public async Task<IActionResult> GetAll()
        {
            var institutions = await this._institutionService.GetAll();
            return this.OkResponse("Instituições listadas com sucesso.", institutions);
        }

        [HttpGet("institutions/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var institution = await this._institutionService.GetById(id);
                return this.OkResponse("Instituição encontrada.", institution);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }

        [HttpPost("institutions")]
        public async Task<IActionResult> Create([FromBody] CreateInstitutionDto dto)
        {
            try
            {
                var created = await this._institutionService.Create(dto);

                return this.CreatedResponse(
                    actionName: nameof(GetById),
                    routeValues: new { id = created.Id },
                    message: "Instituição criada com sucesso.",
                    data: created
                );
            }
            catch (BadRequestException ex)
            {
                return this.BadRequestResponse(ex.Message);
            }
        }

        [HttpPatch("institutions/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInstitutionDto dto)
        {
            try
            {
                var updated = await this._institutionService.Update(id, dto);
                return this.OkResponse("Instituição atualizada com sucesso.", updated);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequestResponse(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }

        [HttpDelete("institutions/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await this._institutionService.Delete(id);
                return this.OkResponse("Instituição removida com sucesso.", deleted);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }
    }
}
