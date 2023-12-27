namespace EmailService.Models
{
    public class SendEmailResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
