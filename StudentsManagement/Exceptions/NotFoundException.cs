using StudentsManagement.Extensions.Common;

namespace StudentsManagement.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public static NotFoundException For(EntityName entity, object key)
        {
            return new NotFoundException($"{entity.ToPtBr()} com identificador '{key}' não foi encontrado.");
        }
    }
}
