using EmailService.Models;
using EmailService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmailServiceClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailService _emailService;

        public IndexModel(ILogger<IndexModel> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public void OnGet()
        {
            var to = new List<EmailAddress> { new EmailAddress("shah.roshni187@gmail.com", "Roshi Shah") };
            var from = new EmailAddress("roshni@visualogyx.com", "Roshni");
            var subject = "Test";
            var body = "<html><body>Hi</body></html>";
            var simpleEmail = new SimpleEmailRequest(to, from, subject, body);
            var res = _emailService.SendEmailAsync(simpleEmail).Result;
        }
    }
}