namespace MyRoot.Application.DTOs
{
    public class Result
    {
        public virtual object? Value { get; set; }
        public string? Message { get; set; }
        public bool Succeeded { get; set; }

        protected Result(bool succeeded, object? value, string? message)
        {
            Succeeded = succeeded;
            Value = value;
            Message = message;
        }

        public static Result Success(object? value = null, string ? message = null)
            => new Result(true, value, message);

        public static Result Failed(string? message = null, object? value = null)
           => new Result(false, value, message);
    }

    public class Result<TValue> : Result
    {
        public new TValue? Value { get; set; }

        protected Result(bool succeeded, TValue? value, string? message)
            : base(succeeded, value, message)
        {
            Value = value;
        }

        public static Result<TValue> Success(TValue? value = default, string ? message = null)
            => new Result<TValue>(true, value, message);

        public static Result<TValue> Failed(string? message = null, TValue? value = default)
            => new Result<TValue>(false, value, message);
    }
}
