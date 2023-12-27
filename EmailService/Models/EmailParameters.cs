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

        /// <summary>
        /// To email address(es). Object containing Name and Email of the
        /// receipient. 
        /// </summary>
        public List<EmailAddress> To { get; set; }

        /// <summary>
        /// From email address. Object containing Name and Email of the
        /// Sender. This email needs to be verified to send email else it
        /// will give exception.
        /// </summary>
        public EmailAddress From { get; set; }

        /// <summary>
        /// Subject of email to be sent. It should be relevant text
        /// to match the content. In case of Templated emails, subject needs
        /// to be passed in template data as well.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Attachments to be sent.
        /// </summary>
        public List<AttachmentData>? Attachments { get; set; }
    }

    public class SimpleEmailRequest : EMailRequest
    {
        public SimpleEmailRequest(List<EmailAddress> to, EmailAddress from, string subject, string body) : base(to, from, subject)
        {
            Body = body;
        }

        /// <summary>
        /// HTML of email to be sent.
        /// </summary>
        public string Body { get; set; }
    }

    public class TemplatedEmailRequest : EMailRequest
    {
        public TemplatedEmailRequest(List<EmailAddress> to, EmailAddress from, string subject, string templateNameOrId, dynamic templateData) : base(to, from, subject)
        {
            TemplateNameOrId = templateNameOrId;
            TemplateData = templateData;
        }

        /// <summary>
        /// In case of AWSSES, it will hold template name.
        /// For SendGrid, it will hold template Id. You can find template id
        /// from https://mc.sendgrid.com/dynamic-templates in sendgrid.
        /// </summary>
        public string TemplateNameOrId { get; set; }

        /// <summary>
        /// In case of AWSSES, it will hold serialized object.
        /// In case of SendGrid, it will hold proper object format.
        /// </summary>
        public dynamic TemplateData { get; set; }

    }

    public class EmailAddress
    {
        public EmailAddress(string email, string name)
        {
            Email = email;
            Name = name;
        }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
