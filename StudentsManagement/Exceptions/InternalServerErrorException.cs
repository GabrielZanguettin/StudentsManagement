namespace StudentsManagement.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message) { }

        public static InternalServerErrorException Unexpected(string? details = null)
        {
            var msg = "Ocorreu um erro interno inesperado.";
            if (!string.IsNullOrWhiteSpace(details))
                msg += $" {details}";
            return new InternalServerErrorException(msg);
        }

        public static InternalServerErrorException Dependency(string dependencyName, string? details = null)
        {
            var msg = $"Falha ao processar a requisição (dependência: {dependencyName}).";
            if (!string.IsNullOrWhiteSpace(details))
                msg += $" {details}";
            return new InternalServerErrorException(msg);
        }
    }
}