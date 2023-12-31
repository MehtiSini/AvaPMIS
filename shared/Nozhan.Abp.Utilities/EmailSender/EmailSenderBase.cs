﻿using System.Net.Mail;
using System.Text;

namespace Nozhan.Abp.Utilities.EmailSender
{
    /// <summary>
    /// This class can be used as base to implement <see cref="IEmailSender"/>.
    /// </summary>
    public abstract class EmailSenderBase
    {
        protected MailConfigOptions ConfigOptions { get; }

        protected EmailSenderBase(MailConfigOptions configOptions)
        {
            ConfigOptions = configOptions;
        }

        public virtual async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage
            {
                To = { to },
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            });
        }

        public virtual async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(new MailMessage(from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        public virtual async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                await NormalizeMailAsync(mail);
            }

            await SendEmailAsync(mail);
        }

        /// <summary>
        /// Should implement this method to send email in derived classes.
        /// </summary>
        /// <param name="mail">Mail to be sent</param>
        protected abstract Task SendEmailAsync(MailMessage mail);

        /// <summary>
        /// Normalizes given email.
        /// Fills <see cref="MailMessage.From"/> if it's not filled before.
        /// Sets encodings to UTF8 if they are not set before.
        /// </summary>
        /// <param name="mail">Mail to be normalized</param>
        protected virtual Task NormalizeMailAsync(MailMessage mail)
        {
            if (mail.From == null || string.IsNullOrWhiteSpace(mail.From.Address))
            {
                mail.From = new MailAddress(ConfigOptions.DefaultFromAddress, ConfigOptions.DefaultFromDisplayName,
                    Encoding.UTF8);
            }

            if (mail.HeadersEncoding == null)
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding == null)
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding == null)
            {
                mail.BodyEncoding = Encoding.UTF8;
            }

            return Task.CompletedTask;
        }
    }
}