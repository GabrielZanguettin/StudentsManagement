namespace StudentsManagement.Extensions.Common
{
    public enum EntityName
    {
        Institution,
        Course,
        Subject,
        Student
    }

    public static class EntityNameExtensions
    {
        public static string ToPtBr(this EntityName entity)
        {
            return entity switch
            {
                EntityName.Institution => "Instituição",
                EntityName.Course => "Curso",
                EntityName.Subject => "Matéria",
                EntityName.Student => "Aluno",
                _ => "Recurso"
            };
        }
    }
}