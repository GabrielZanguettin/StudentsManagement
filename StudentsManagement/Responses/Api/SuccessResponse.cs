namespace StudentsManagement.Responses.Api
{
    public sealed class SuccessResponse<T>
    {
        public string Message { get; init; }
        public T? Data { get; init; }
    }
}
