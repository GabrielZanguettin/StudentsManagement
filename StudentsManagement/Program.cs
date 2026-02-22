using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StudentsManagement.Data;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Interfaces.IServices;
using StudentsManagement.Repositories;
using StudentsManagement.Services;
using StudentsManagement.Validations.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = DatabaseConfigurationValidator.ValidateDefaultConnection(builder.Configuration);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
builder.Services.AddScoped<IInstitutionService, InstitutionService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
