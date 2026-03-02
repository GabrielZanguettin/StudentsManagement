namespace StudentsManagement.Exceptions
{
    public sealed class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }

        public static ConfigurationException MissingSection(string sectionName)
            => new ConfigurationException($"Configuração '{sectionName}' não encontrada.");

        public static ConfigurationException MissingValue(string keyPath)
            => new ConfigurationException($"Configuração obrigatória '{keyPath}' não encontrada ou está vazia.");
    }
}