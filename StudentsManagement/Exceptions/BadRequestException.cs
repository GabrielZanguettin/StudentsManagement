namespace StudentsManagement.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }

        public static BadRequestException RequiredFields(params string[] fields)
        {
            var list = string.Join(", ", fields);
            return new BadRequestException($"Campos obrigatórios: {list}.");
        }
    }
}