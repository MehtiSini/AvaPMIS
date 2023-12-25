using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;

namespace Nozhan.Abp.Utilities.EmailSender
{
    public class AbpMailKitOptions
    {
        public SecureSocketOptions? SecureSocketOption { get; set; }
    }
}
