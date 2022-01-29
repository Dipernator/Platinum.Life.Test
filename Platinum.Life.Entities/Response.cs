namespace Platinum.Life.Entities
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Entity { get; set; }
    }

    public enum ResponseMessage { 
        Success,
        NotUpdated,
        NotInserted,
        NotDeleted,
        NotFound
    }
}
