﻿using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

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
                From = new MailAddress("novi8irtest@gmail.com"),
                Subject = "Email Sender App",
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

            return "Email Sent Successfully!";
        }
        catch (System.Exception e)
        {
            return e.Message;
        }

    }
}