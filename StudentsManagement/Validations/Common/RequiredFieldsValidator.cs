namespace StudentsManagement.Validations.Common
{
    public static class RequiredFieldsValidator
    {
        public static string[] GetMissing(params (string Field, bool IsMissing)[] checks)
        {
            return checks.Where(c => c.IsMissing).Select(c => c.Field).ToArray();
        }
    }
}