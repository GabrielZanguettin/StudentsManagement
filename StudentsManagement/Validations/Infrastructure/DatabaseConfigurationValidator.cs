using StudentsManagement.Exceptions;

namespace StudentsManagement.Validations.Infrastructure
{
    public static class DatabaseConfigurationValidator
    {
        public static string ValidateDefaultConnection(IConfiguration config)
        {
            var cs = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(cs))
                throw ConfigurationException.MissingValue("ConnectionStrings:DefaultConnection");

            return cs;
        }
    }
}