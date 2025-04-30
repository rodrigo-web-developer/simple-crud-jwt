using System.ComponentModel.DataAnnotations;

namespace StoreAPI.DTOs
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public List<ValidationError> Errors { get; private set; } = new();
        public object Data { get; set; }

        public T Get<T>() where T : class
        {
            if (Data is T data)
                return data;
            throw new InvalidOperationException($"Data is not of type {typeof(T).Name}");
        }

        public static OperationResult Ok(object data = null) =>
            new() { Success = true, Data = data };

        public static OperationResult Fail(ValidationResult[] errors) =>
            new()
            {
                Success = false,
                Errors = errors.Select(e =>
                    new ValidationError(e.ErrorMessage, e.MemberNames.FirstOrDefault())
                ).ToList()
            };
        public static OperationResult FailException(Exception exception) =>
            new()
            {
                Success = false,
                Errors = [
                    new ValidationError(exception.Message, $"{exception.GetType().Name} - {exception.Message}")
                ]
            };
    }

    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
