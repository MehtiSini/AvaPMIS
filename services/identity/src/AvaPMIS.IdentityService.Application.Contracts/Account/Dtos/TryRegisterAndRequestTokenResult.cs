using System;
using Volo.Abp.MultiTenancy;

namespace AvaPMIS.IdentityService.Account.Dtos
{
    [Serializable]
    public class TryRegisterAndRequestTokenResult : IMultiTenant
    {
        public string Token { get;}

        public RegisterResult Result { get; }

        public string Description => Result.ToString();

        public Guid? TenantId { get; }

        public TryRegisterAndRequestTokenResult(RegisterResult result, string token, Guid? tenantId = null)
        {
            Result = result;
            Token = token;
            TenantId = tenantId;
        }
    }
}