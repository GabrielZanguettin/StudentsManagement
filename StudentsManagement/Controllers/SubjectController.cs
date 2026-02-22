using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Dtos.Subjects;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Api;
using StudentsManagement.Interfaces.IServices;

namespace StudentsManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            this._subjectService = subjectService;
        }

        [HttpGet("subjects")]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await this._subjectService.GetAll();
            return this.OkResponse("Matérias listadas com sucesso.", subjects);
        }

        [HttpGet("subjects/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var subject = await this._subjectService.GetById(id);
                return this.OkResponse("Matéria encontrada.", subject);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }

        [HttpPost("subjects")]
        public async Task<IActionResult> Create([FromBody] CreateSubjectDto dto)
        {
            try
            {
                var created = await this._subjectService.Create(dto);

                return this.CreatedResponse(
                    actionName: nameof(GetById),
                    routeValues: new { id = created.Id },
                    message: "Matéria criada com sucesso.",
                    data: created
                );
            }
            catch (BadRequestException ex)
            {
                return this.BadRequestResponse(ex.Message);
            }
        }

        [HttpPatch("subjects/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubjectDto dto)
        {
            try
            {
                var updated = await this._subjectService.Update(id, dto);
                return this.OkResponse("Matéria atualizada com sucesso.", updated);
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

        [HttpDelete("subjects/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await this._subjectService.Delete(id);
                return this.OkResponse("Matéria removida com sucesso.", deleted);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }
    }
}
