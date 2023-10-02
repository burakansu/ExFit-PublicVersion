namespace Data.Custom
{
    public class Response
    {
        public bool Success { get; set; }
        public string Description { get; set; }
        public Exception ex { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data;
    }
}
