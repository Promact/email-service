using EmailService.Models;
using EmailService.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmailServiceClient.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public TestController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var to = new List<EmailAddress> { new EmailAddress("shah.roshni187@gmail.com", "Roshi Shah") };
            var from = new EmailAddress("roshni@visualogyx.com");
            var subject = "Test";
            var body = "<html><body>Hi</body></html>";
            var simpleEmail = new SimpleEmailRequest(to, from, subject, body);
            await _emailService.SendEmailAsync(simpleEmail);

            return Ok();
        }
    }
}
