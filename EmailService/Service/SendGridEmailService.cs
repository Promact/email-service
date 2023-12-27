using EmailService.Models;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;

namespace EmailService.Service
{
    public class SendGridEmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;

        public SendGridEmailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task<SendEmailResult> SendEmailAsync(SimpleEmailRequest simpleEmailRequest)
        {
            try
            {
                var message = new SendGridMessage
                {
                    From = new SendGrid.Helpers.Mail.EmailAddress(simpleEmailRequest.From.Email, simpleEmailRequest.From.Name),
                    Subject = simpleEmailRequest.Subject,
                    HtmlContent = simpleEmailRequest.Body
                };

                simpleEmailRequest.To.ForEach(emailAddress =>
                {
                    message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(emailAddress.Email, emailAddress.Name));
                });

                if (simpleEmailRequest.Attachments != null && simpleEmailRequest.Attachments.Any())
                {
                    foreach (var item in simpleEmailRequest.Attachments)
                    {
                        message.AddAttachment(item.FileName, Convert.ToBase64String(item.Content), item.ContentType);
                    }

                }
                var response = await _sendGridClient.SendEmailAsync(message);
                if (response.IsSuccessStatusCode) return new SendEmailResult { IsSuccess = true };
                return new SendEmailResult { IsSuccess = false, ErrorMessage = await response.Body.ReadAsStringAsync() };
            }
            catch (SendGridInternalException ex)
            {
                return new SendEmailResult
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            catch (Exception ex)
            {
                return new SendEmailResult
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<SendEmailResult> SendTemplatedEmailAsync(TemplatedEmailRequest templatedEmailRequest)
        {
            try
            {
                var message = new SendGridMessage
                {
                    From = new SendGrid.Helpers.Mail.EmailAddress(templatedEmailRequest.From.Email, templatedEmailRequest.From.Name),
                    Subject = templatedEmailRequest.Subject
                };

                message.SetTemplateId(templatedEmailRequest.TemplateNameOrId);
                message.SetTemplateData(templatedEmailRequest.TemplateData);

                templatedEmailRequest.To.ForEach(emailAddress =>
                {
                    message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(emailAddress.Email, emailAddress.Name));
                });

                if (templatedEmailRequest.Attachments != null && templatedEmailRequest.Attachments.Any())
                {
                    foreach (var item in templatedEmailRequest.Attachments)
                    {
                        message.AddAttachment(item.FileName, Convert.ToBase64String(item.Content), item.ContentType);
                    }

                }
                var response = await _sendGridClient.SendEmailAsync(message);
                if (response.IsSuccessStatusCode) return new SendEmailResult { IsSuccess = true };
                return new SendEmailResult { IsSuccess = false, ErrorMessage =await response.Body.ReadAsStringAsync() };
            }
            catch (SendGridInternalException ex)
            {
                return new SendEmailResult
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            catch (Exception ex)
            {
                return new SendEmailResult
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
        }
    }
}
