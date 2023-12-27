
using EmailService.Models;

namespace EmailService.Service
{
    public interface IEmailService
    {
        Task<SendEmailResult> SendEmailAsync(SimpleEmailRequest simpleEmailRequest);
        Task<SendEmailResult> SendTemplatedEmailAsync(TemplatedEmailRequest templatedEmailRequest);
    }
}
