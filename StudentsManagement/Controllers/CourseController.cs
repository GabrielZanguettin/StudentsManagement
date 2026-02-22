using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Dtos.Courses;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Api;
using StudentsManagement.Interfaces.IServices;

namespace StudentsManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetAll()
        {
            var courses = await this._courseService.GetAll();
            return this.OkResponse("Cursos listados com sucesso.", courses);
        }

        [HttpGet("courses/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var course = await this._courseService.GetById(id);
                return this.OkResponse("Curso encontrado.", course);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }

        [HttpPost("courses")]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            try
            {
                var created = await this._courseService.Create(dto);

                return this.CreatedResponse(
                    actionName: nameof(GetById),
                    routeValues: new { id = created.Id },
                    message: "Curso criado com sucesso.",
                    data: created
                );
            }
            catch (BadRequestException ex)
            {
                return this.BadRequestResponse(ex.Message);
            }
        }

        [HttpPatch("courses/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseDto dto)
        {
            try
            {
                var updated = await this._courseService.Update(id, dto);
                return this.OkResponse("Curso atualizado com sucesso.", updated);
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

        [HttpDelete("courses/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await this._courseService.Delete(id);
                return this.OkResponse("Curso removido com sucesso.", deleted);
            }
            catch (NotFoundException ex)
            {
                return this.NotFoundResponse(ex.Message);
            }
        }
    }
}
