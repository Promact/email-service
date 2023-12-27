using EmailService.Models;
using EmailService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
            // Simple email (SES and SendGrid)
            //var to = new List<EmailAddress> { new EmailAddress("shah.roshni187@gmail.com", "Roshi Shah") };
            //var from = new EmailAddress("roshni@promactinfo.com", "Roshni");
            //var subject = "Test";
            //var body = "<html><body>Hi</body></html>";
            //var simpleEmail = new SimpleEmailRequest(to, from, subject, body);
            //var res = _emailService.SendEmailAsync(simpleEmail).Result;

            // Templated email SES
            //var to = new List<EmailAddress> { new EmailAddress("shah.roshni187@gmail.com", "Roshi Shah") };
            //var from = new EmailAddress("roshni@visualogyx.com");
            //var subject = "Test";
            //var simpleEmail = new TemplatedEmailRequest(to, from, subject, "TestTemplate",
            //JsonConvert.SerializeObject(new { Subject = "Templated Email", name = "Roshni" }));
            //var res = _emailService.SendTemplatedEmailAsync(simpleEmail).Result;

            //Templated email SendGrid
            //var to = new List<EmailAddress> { new EmailAddress("shah.roshni187@gmail.com", "Roshi Shah") };
            //var from = new EmailAddress("roshni@promactinfo.com", "Roshni");
            //var subject = "Test";
            //var simpleEmail = new TemplatedEmailRequest(to, from, subject, "d-ed0590c396c4a4b89374cc64ed30885",
            //new { Subject = "Templated Email", name = "Roshni" });
            //var res = _emailService.SendTemplatedEmailAsync(simpleEmail).Result;
        }
    }
}