namespace WebAssemblyGameTemplate.Server.Models
{
    public class ValidationResult
    {
        public bool Valid { get; set; }
        public string Reason { get; set; }

        public void SetResult(string reason)
        {
            Valid = false;
            Reason = reason;
        }
    }
}