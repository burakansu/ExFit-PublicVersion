using System.Text.Json;

namespace Core.ErrorHandling
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public bool Success { get; set; }
        public Exception ex { get; set; }
        public override string ToString(){ return JsonSerializer.Serialize(this); }
    }
}
