using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Dtos.Students;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Api;
using StudentsManagement.Interfaces.IServices;

namespace StudentsManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetAll()
        {
            var students = await this._studentService.GetAll();
            return this.OkResponse("Alunos listados com sucesso.", students);
        }

        [HttpGet("students/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var student = await this._studentService.GetById(id);
                return this.OkResponse("Aluno encontrado.", student);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }

        [HttpPost("students")]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            try
            {
                var created = await this._studentService.Create(dto);

                return this.CreatedResponse(
                    actionName: nameof(GetById),
                    routeValues: new { id = created.Id },
                    message: "Aluno matriculado com sucesso.",
                    data: created
                );
            }
            catch (BadRequestException ex)
            {
                return this.BadRequestResponse(ex.Message);
            }
        }

        [HttpPatch("students/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentDto dto)
        {
            try
            {
                var updated = await this._studentService.Update(id, dto);
                return this.OkResponse("Aluno atualizado com sucesso.", updated);
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

        [HttpDelete("students/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await this._studentService.Delete(id);
                return this.OkResponse("Aluno desmatriculado com sucesso.", deleted);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }
    }
}
