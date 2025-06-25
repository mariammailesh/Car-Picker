using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace Car_Picker_API.Helpers
{
    public static class MailingHelper
    {
        public static async Task SendEmail(string email, string code, string title, string message)
        {
            var apiKey = "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";
            /* var options = new SendGridClientOptions
        {
            ApiKey = apiKey
        };
        options.SetDataResidency("eu"); 
        var client = new SendGridClient(options); */
            // uncomment the above 6 lines if you are sending mail using a regional EU subuser
            // and remove the client declaration just below
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("reemnael2001@gmail.com", "CarPickerApp");
            var subject = title;
            var to = new EmailAddress(email, "Car Picker User");
            var plainTextContent = $"Dear User {message} Please Use the Following OTP Code {code} It Will be Expaierd with 2 Minutes ";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, "");
            var response = await client.SendEmailAsync(msg);
        }
    }
}

