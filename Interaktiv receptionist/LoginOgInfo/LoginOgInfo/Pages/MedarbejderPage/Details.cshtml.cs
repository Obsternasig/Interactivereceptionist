using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Medarbejdere;
using LoginOgInfo.Models;
using System.Net;
using System.Net.Mail;

namespace LoginOgInfo.Pages.MedarbejderPage
{
    public class DetailsModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public DetailsModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public MedarbejderInfo MedarbejderInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MedarbejderInfo = await _context.MedarbejderInfo.FirstOrDefaultAsync(m => m.ID == id);

            if (MedarbejderInfo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
    public class MailSenderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string SendEmail(string Name, string Email, string Message)
        {

            try
            {
                // Credentials
                var credentials = new NetworkCredential("novi8irtest@gmail.com", "MAjjDA0218");

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("Novi8irtest@gmail.com"),
                    Subject = "Gæst Ankommet",
                    Body = Message
                };

                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(Email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);

                return "Notifikation sendt";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

        }
    }
}
