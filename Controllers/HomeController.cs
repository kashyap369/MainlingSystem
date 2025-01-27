using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using MailingSystem01.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailingSystem01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", model);
            }

            try
            {
                // SMTP Client Configuration
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("testdemonio0@gmail.com", "Testgmail27c@?"),
                    EnableSsl = true,
                };

                // Email Message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("testdemonio0@gmail.com"),
                    Subject = model.Subject,
                    Body = $"Name: {model.Name}\nEmail: {model.Email}\n\nMessage:\n{model.Message}",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add("shubhamdeveloper27@gmail.com"); // Replace with the recipient's email

                smtpClient.Send(mailMessage);

                ViewBag.Message = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error sending email: {ex.Message}";
            }

            return View("Contact");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
