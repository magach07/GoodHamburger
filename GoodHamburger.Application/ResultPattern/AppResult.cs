namespace GoodHamburger.Application.ResultPattern
{
    public class AppResult<T>
    {
        public bool Success { get; private set; }
        public string? Error { get; private set; }
        public T? Data { get; private set; }

        public static AppResult<T> Ok(T data) => new()
        {
            Success = true,
            Data = data
        };

        public static AppResult<T> Fail(string error) => new()
        {
            Success = false,
            Error = error
        };
    }
}