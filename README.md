# StudentsManagement API

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-blueviolet?style=for-the-badge&logo=dotnet" />
  <img src="https://img.shields.io/badge/C%23-Developer-239120?style=for-the-badge&logo=csharp" />
  <img src="https://img.shields.io/badge/MySQL-Database-4479A1?style=for-the-badge&logo=mysql&logoColor=white" />
  <img src="https://img.shields.io/badge/EF%20Core-Migrations-success?style=for-the-badge&logo=entity-framework" />
  <img src="https://img.shields.io/badge/Scalar-API%20Docs-00C7B7?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Postman-Collection-FF6C37?style=for-the-badge&logo=postman&logoColor=white" />
</p>

API REST desenvolvida em .NET 10 para gerenciamento de estudantes, cursos, disciplinas e instituições.

Implementa arquitetura em camadas com padrões de repositório e serviços, utilizando Entity Framework Core para persistência de dados e MySQL como banco de dados.

---

## Stack Técnica

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- C#

---

## Estrutura do Projeto

```bash
StudentsManagement
├── Appliers
│   ├── Courses/
│   │   └── CoursePatchApplier.cs
│   ├── Institutions/
│   │   └── InstitutionPatchApplier.cs
│   ├── Students/
│   │   └── StudentPatchApplier.cs
│   └── Subjects/
│       └── SubjectPatchApplier.cs
├── Controllers
│   ├── StudentController.cs
│   ├── CourseController.cs
│   ├── InstitutionController.cs
│   └── SubjectController.cs
├── Data
│   └── DataContext.cs
├── DTOs
│   ├── Courses/
│   │   ├── CourseResponseDto.cs
│   │   ├── CreateCourseDto.cs
│   │   └── UpdateCourseDto.cs
│   ├── Institutions/
│   │   ├── CreateInstitutionDto.cs
│   │   ├── InstitutionResponseDto.cs
│   │   └── UpdateInstitutionDto.cs
│   ├── Students/
│   │   ├── CreateStudentDto.cs
│   │   ├── StudentResponseDto.cs
│   │   └── UpdateStudentDto.cs
│   └── Subjects/
│       ├── CreateSubjectDto.cs
│       ├── SubjectResponseDto.cs
│       └── UpdateSubjectDto.cs
├── Entities
│   ├── Student.cs
│   ├── Course.cs
│   ├── Institution.cs
│   ├── Subject.cs
│   └── CourseSubject.cs
├── Exceptions
│   ├── BadRequestException.cs
│   ├── ConfigurationException.cs
│   ├── InternalServerErrorException.cs
│   └── NotFoundException.cs
├── Extensions
│   ├── Api/
│   │   └── ControllerResponseExtensions.cs
│   └── Common/
│       └── EntityNameExtensions.cs
├── Interfaces
│   ├── IRepositories/
│   │   ├── ICourseRepository.cs
│   │   ├── IInstitutionRepository.cs
│   │   ├── IStudentRepository.cs
│   │   └── ISubjectRepository.cs
│   └── IServices/
│       ├── ICourseService.cs
│       ├── IInstitutionService.cs
│       ├── IStudentService.cs
│       └── ISubjectService.cs
├── Mappings
│   ├── Courses/
│   │   └── CourseMappings.cs
│   ├── Institutions/
│   │   └── InstitutionMappings.cs
│   ├── Students/
│   │   └── StudentMappings.cs
│   └── Subjects/
│       └── SubjectMappings.cs
├── Migrations
│   ├── 20260220021954_Init.cs
│   ├── 20260220021954_Init.Designer.cs
│   ├── 20260221200705_AddInstitution.cs
│   └── ...
├── Repositories
│   ├── StudentRepository.cs
│   ├── CourseRepository.cs
│   ├── InstitutionRepository.cs
│   └── SubjectRepository.cs
├── Responses
│   ├── SuccessResponse.cs
│   └── ErrorResponse.cs
├── Services
│   ├── StudentService.cs
│   ├── CourseService.cs
│   ├── InstitutionService.cs
│   └── SubjectService.cs
├── Validations
│   ├── Api/
│   │   └── PatchValidator.cs
│   ├── Common/
│   │   ├── RequiredFieldsValidator.cs
│   │   ├── StringLengthValidator.cs
│   │   └── StringTrimmer.cs
│   ├── Domain/
│   │   ├── Courses/
│   │   │   └── CourseValidator.cs
│   │   ├── Institutions/
│   │   │   └── InstitutionValidator.cs
│   │   ├── Students/
│   │   │   └── StudentValidator.cs
│   │   └── Subjects/
│   │       └── SubjectValidator.cs
│   └── Infrastructure/
│       └── DatabaseConfigurationValidator.cs
├── Properties/
│   └── launchSettings.json
├── appsettings.json
├── appsettings.Development.json
├── endpoints.json
├── StudentsManagement.csproj
├── StudentsManagement.csproj.user
├── StudentsManagement.http
└── Program.cs
```

---

## Arquitetura

### Entities
Modelos de domínio representando as entidades do sistema: Student, Course, Institution, Subject e CourseSubject.

### DTOs
Data Transfer Objects para controlar entrada e saída de dados, separados por contexto (Students, Courses, Institutions, Subjects).

### Repositories
Camada de acesso a dados utilizando Entity Framework Core, isolando a lógica de persistência.

### Services
Lógica de negócio e orquestração entre repositories e controllers.

### Controllers
Endpoints REST com operações CRUD completas para cada entidade.

### Appliers
Aplicadores de patches específicos por entidade, responsáveis por aplicar atualizações parciais (PATCH) de forma segura e validada.

### Mappings
Conversões entre Entities e DTOs organizadas por domínio.

### Extensions
Métodos de extensão customizados para APIs e funcionalidades comuns, promovendo reusabilidade de código.

### Responses & Exceptions
Padronização de respostas HTTP e tratamento de exceções customizado.

### Validations
Validadores organizados em camadas (Api, Common, Domain, Infrastructure) para garantir integridade dos dados em diferentes níveis da aplicação.

---

## Configuração do Banco de Dados

Configure a connection string no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=students_management;user=root;password=senha123;"
  }
}
```

---

## Como Executar

### 1. Restaurar pacotes
```bash
dotnet restore
```

### 2. Aplicar as migrations ao banco MySQL
```bash
dotnet ef database update
```

### 3. Rodar o projeto
```bash
dotnet run
```

A API estará disponível em `https://localhost:8080` ou conforme configurado em `launchSettings.json`.

---

## Endpoints

A documentação completa está disponível através do Scalar em `http://localhost:8080/scalar` após executar o projeto.

O arquivo `endpoints.json` contém uma collection do Postman com todos os endpoints prontos para teste.

Principais recursos:
- `/api/students` - Gerenciamento de estudantes
- `/api/courses` - Gerenciamento de cursos
- `/api/institutions` - Gerenciamento de instituições
- `/api/subjects` - Gerenciamento de matérias
