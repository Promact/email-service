﻿using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using EmailService.Models;

namespace EmailService.Service
{
    public class AwsEmailService : IEmailService
    {
        private readonly IAmazonSimpleEmailService _sesClient;

        public AwsEmailService(IAmazonSimpleEmailService sesClient)
        {
            _sesClient = sesClient;
        }

        public async Task<SendEmailResult> SendEmailAsync(SimpleEmailRequest simpleEmailRequest)
        {
            try
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = $"{simpleEmailRequest.From.Name} <{simpleEmailRequest.From.Email}>",
                    Message = new Message
                    {
                        Body = new Body { Html = new Content { Data = simpleEmailRequest.Body } },
                        Subject = new Content { Data = simpleEmailRequest.Subject },
                    },
                    Destination = new Destination { ToAddresses = new List<string>() }
                };
                sendRequest.Destination.ToAddresses.AddRange(simpleEmailRequest.To.Select(x => x.Email));

                var response = await _sesClient.SendEmailAsync(sendRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return new SendEmailResult { IsSuccess = true };
                return new SendEmailResult { IsSuccess = false };
            }
            catch (AmazonSimpleEmailServiceException ex)
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
                var sendRequest = new SendTemplatedEmailRequest
                {
                    Source = $"{templatedEmailRequest.From.Name} <{templatedEmailRequest.From.Email}>",
                    Template = templatedEmailRequest.TemplateNameOrId,
                    TemplateData = templatedEmailRequest.TemplateData,
                    Destination = new Destination { ToAddresses = new List<string>() }
                };

                sendRequest.Destination.ToAddresses.AddRange(templatedEmailRequest.To.Select(x => x.Email));

                var response = await _sesClient.SendTemplatedEmailAsync(sendRequest);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return new SendEmailResult { IsSuccess = true };
                return new SendEmailResult { IsSuccess = false };
            }
            catch (AmazonSimpleEmailServiceException ex)
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

        /// <summary>
        /// Utility method to check if the test data is valid or not for AWS template.
        /// </summary>
        /// <param name="macros"></param>
        /// <param name="templateName"></param>
        private static void TestTemplate(string macros, string templateName)
        {
            try
            {
                using (var sesClient = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1))
                {
                    var testTemplateRequest = new TestRenderTemplateRequest
                    {
                        TemplateData = macros,
                        TemplateName = templateName
                    };
                    var response = sesClient.TestRenderTemplateAsync(testTemplateRequest).Result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
