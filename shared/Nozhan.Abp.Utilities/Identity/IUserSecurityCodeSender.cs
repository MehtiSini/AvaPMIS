using System.Linq.Dynamic.Core.Tokenizer;
using System.Numerics;

namespace Nozhan.Abp.Utilities.Identity
{
    public interface IUserSecurityCodeSender
    {
        Task<bool> SendAsync(VerificationSendingMethod method,string receivr,string template, string code, VerificationCodeType type, object textTemplateModel = null);
    }
}
