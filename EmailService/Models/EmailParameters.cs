namespace EmailService.Models
{
    public class EMailRequest
    {
        public EMailRequest(List<EmailAddress> to, EmailAddress from, string subject)
        {
            To = to;
            From = from;
            Subject = subject;
        }
        public List<EmailAddress> To { get; set; }
        public EmailAddress From { get; set; }
        public string Subject { get; set; }
        public List<AttachmentData>? Attachments { get; set; }
    }

    public class SimpleEmailRequest : EMailRequest
    {
        public SimpleEmailRequest(List<EmailAddress> to, EmailAddress from, string subject, string body) : base(to, from, subject)
        {
            Body = body;
        }
        public string Body { get; set; }
    }

    public class TemplatedEmailRequest : EMailRequest
    {
        public TemplatedEmailRequest(List<EmailAddress> to, EmailAddress from, string subject, string templateNameOrId, string templateData) : base(to, from, subject)
        {
            TemplateNameOrId = templateNameOrId;
            TemplateData = templateData;
        }

        public string TemplateNameOrId { get; set; }
        public string TemplateData { get; set; }

    }

    public class EmailAddress
    {
        public EmailAddress(string email)
        {
            Email = email;
        }

        public EmailAddress(string email, string name)
        {
            Email = email;
            Name = name;
        }
        public string? Name { get; set; }
        public string Email { get; set; }
    }
}
