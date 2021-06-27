using Dapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;

namespace TagTeam.Admin.Service
{
    public class emailService
    {
        private readonly string _Mail;
        private readonly string _DisplayName;
        private readonly string _Password;
        private readonly string _Host;
        private readonly int _Port;

        public emailService(string mail, string displayName, string password, string host, int port)
        {
            _Mail = mail;
            _DisplayName = displayName;
            _Password = password;
            _Host = host;
            _Port = port;

        }

        public async Task<BaseModel> SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                //if (mailRequest.Attachments != null)
                //{
                //    byte[] fileBytes;
                //    foreach (var file in mailRequest.Attachments)
                //    {
                //        if (file.Length > 0)
                //        {
                //            using (var ms = new MemoryStream())
                //            {
                //                file.CopyTo(ms);
                //                fileBytes = ms.ToArray();
                //            }
                //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                //        }
                //    }
                //}
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_Host, _Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_Mail, _Password);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
                return new BaseModel() { code = "1000", description = "success", data = null };
            }
            catch (Exception ex)
            {
                return new BaseModel() { code = "998", description = ex.Message, data = null };
            }
        }
    }
}
