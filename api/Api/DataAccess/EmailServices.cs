using Api.Model;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Api.DataAccess
{
    public class EmailServices : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        public EmailServices(IOptions<EmailConfig> emailconfig)
        {
            _emailConfig = emailconfig.Value;
        }
        public async Task<ResponseModel<bool>> SendEmail(EmailParam emailParameters)
        {
            try
            {
                var client = new SendGridClient(_emailConfig.ApiKey);
                var from = new EmailAddress(_emailConfig.FromEmail, _emailConfig.FromEmailAlias);
                var subject = emailParameters.EmailSubject;
                var to = new EmailAddress(emailParameters.EmailRecipient);
                var plainTextContent = "Test";
                var mailBody = emailParameters.EmailBody;
                var htmlContent = mailBody;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);


                if (!string.IsNullOrWhiteSpace(emailParameters.EmailCarbonCopies))
                {
                    List<EmailAddress> emailAddresses = new List<EmailAddress>();
                    foreach (string emailAddress in emailParameters.EmailCarbonCopies.Split(','))
                    {
                        if (!string.IsNullOrWhiteSpace(emailAddress)
                            && !emailAddress.Equals(emailParameters.EmailRecipient, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!emailAddresses.Contains(new EmailAddress(emailAddress)))
                                emailAddresses.Add(new EmailAddress(emailAddress));
                        }
                    }
                    if (emailAddresses.Count > 0)
                        msg.AddCcs(emailAddresses);
                }

                var response = await client.SendEmailAsync(msg);
                if (response.IsSuccessStatusCode)
                {
                    return new ResponseModel<bool>()
                    {
                        IsError = false,
                        Message = "Email sent successfully",
                        Data = true
                    };
                }
                else
                {
                    return new ResponseModel<bool>()
                    {
                        IsError = true,
                        Message = "Error while sending Email",
                        Data = false
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseModel<bool>()
                {
                    IsError = true,
                    Message = "Error while sending Email",
                    Data = false
                };
            }
        }
    }
}
