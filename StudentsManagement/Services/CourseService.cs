using StudentsManagement.Appliers.Courses;
using StudentsManagement.Dtos.Courses;
using StudentsManagement.Entities;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Common;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Interfaces.IServices;
using StudentsManagement.Mappings.Courses;
using StudentsManagement.Validations.Common;
using StudentsManagement.Validations.Domain.Courses;

namespace StudentsManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly ISubjectRepository _subjectRepository;

        public CourseService(
            ICourseRepository courseRepository,
            IInstitutionRepository institutionRepository,
            ISubjectRepository subjectRepository)
        {
            _courseRepository = courseRepository;
            _institutionRepository = institutionRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<List<CourseResponseDto>> GetAll()
        {
            var courses = await _courseRepository.GetAll();

            return courses
                .Select(c => c.ToCourseResponseDto())
                .ToList();
        }

        public async Task<CourseResponseDto> GetById(Guid id)
        {
            var course = await _courseRepository.GetById(id);
            if (course is null)
                throw NotFoundException.For(EntityName.Course, id);

            return course.ToCourseResponseDto();
        }

        public async Task<CourseResponseDto> Create(CreateCourseDto dto)
        {
            var missing = RequiredFieldsValidator.GetMissing(
                (nameof(dto.Name), string.IsNullOrWhiteSpace(dto.Name)),
                (nameof(dto.InstitutionId), dto.InstitutionId == Guid.Empty)
            );

            if (missing.Length > 0)
                throw BadRequestException.RequiredFields(missing);

            PatchValidator.EnsureNoEmptyGuids(nameof(dto.SubjectIds), dto.SubjectIds);

            var name = dto.Name.Trim();
            var institutionId = dto.InstitutionId;
            var subjectIds = dto.SubjectIds?.Distinct().ToList();

            await CourseValidator.ValidateCreateAsync(
                name,
                institutionId,
                subjectIds,
                _institutionRepository,
                _subjectRepository
            );

            var courseId = Guid.NewGuid();

            var course = new Course
            {
                Id = courseId,
                Name = name,
                InstitutionId = institutionId,
                CourseSubjects = subjectIds?
                    .Select(subjectId => new CourseSubject
                    {
                        CourseId = courseId,
                        SubjectId = subjectId
                    })
                    .ToList()
                    ?? new List<CourseSubject>()
            };

            var created = await _courseRepository.Create(course);

            return await this.GetById(created.Id);
        }

        public async Task<CourseResponseDto> Update(Guid id, UpdateCourseDto dto)
        {
            var course = await _courseRepository.GetById(id);
            if (course is null)
                throw NotFoundException.For(EntityName.Course, id);

            PatchValidator.EnsureHasAny(dto.Name, dto.InstitutionId, dto.SubjectIds);
            PatchValidator.EnsureNotBlank(nameof(dto.Name), dto.Name);
            PatchValidator.EnsureNotEmptyGuid(nameof(dto.InstitutionId), dto.InstitutionId);
            PatchValidator.EnsureNoEmptyGuids(nameof(dto.SubjectIds), dto.SubjectIds);

            var subjectIds = dto.SubjectIds?.Distinct().ToList();

            await CourseValidator.ValidateUpdateAsync(
                dto.Name,
                dto.InstitutionId,
                subjectIds,
                _institutionRepository,
                _subjectRepository
            );

            CoursePatchApplier.ApplyCoursePatch(course, dto);

            var updatedCourse = await _courseRepository.Update(course);

            await _courseRepository.SyncSubjects(course.Id, subjectIds);

            return await this.GetById(updatedCourse.Id);
        }

        public async Task<CourseResponseDto> Delete(Guid id)
        {
            var course = await _courseRepository.GetById(id);
            if (course is null)
                throw NotFoundException.For(EntityName.Course, id);

            await _courseRepository.Delete(course);

            return course.ToCourseResponseDto();
        }
    }
}