using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace TaskControl.Helpers
{
    public static class Utilities
    {
        /// <summary>
        /// Send email function
        /// </summary>
        /// <param name="userMail"></param>
        /// <param name="varsubject"></param>
        /// <param name="varbody"></param>
        public static bool SendEmail(string userMail, string varsubject, string varbody)
        {
            try
            {
                var fromAddress = new MailAddress("fernando.oliva.hueto@gmail.com", "From Name");
                var toAddress = new MailAddress(userMail, "To Name");
                const string fromPassword = "Pr0gr@mador";
                string subject = varsubject;
                string body = varbody;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 15000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}