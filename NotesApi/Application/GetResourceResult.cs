namespace NotesApi.Application
{
    public class GetResourceResult<T> where T:class
    {
        private readonly SearchResultStatus _status;

        private GetResourceResult(SearchResultStatus status, T value)
        {
            _status = status;
            Value = value;
        }

        static GetResourceResult()
        {
            ResourceNotFound = new GetResourceResult<T>(SearchResultStatus.NotFound, null);
        }

        public T Value { get; }
        public static GetResourceResult<T> ResourceNotFound { get; }
        public bool IsNotFound => _status == SearchResultStatus.NotFound;
        public bool IsSuccess => _status == SearchResultStatus.Success;

        public static GetResourceResult<T> Success(T result)
        {
            return new GetResourceResult<T>(SearchResultStatus.Success, result);
        }

        enum SearchResultStatus
        {
            None,
            NotFound,
            Success
        }
    }
}