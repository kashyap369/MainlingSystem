using MailingSystem01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MailingSystem01.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create a new MailMessage
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(model.Email),
                        Subject = model.Subject,
                        Body = $"Name: {model.Name}\n\nMessage:\n{model.Message}",
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add("testdemonio0@gmail.com"); // Your email address

                    // Configure the SMTP client
                    using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.Credentials = new NetworkCredential("testdemonio0@gmail.com", ""); // Replace with your email and app-specific password
                        smtpClient.EnableSsl = true;

                        // Send the email
                        smtpClient.Send(mailMessage);
                    }

                    ViewBag.Message = "Email sent successfully!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Failed to send email. Error: {ex.Message}";
                }
            }
            else
            {
                ViewBag.Message = "Please fill in all required fields.";
            }

            return View("Index");
        }
    }
}
