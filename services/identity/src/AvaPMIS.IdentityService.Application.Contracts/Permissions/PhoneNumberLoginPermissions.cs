using Volo.Abp.Reflection;

namespace AvaPMIS.IdentityService.Permissions
{
    public class PhoneNumberLoginPermissions
    {
        public const string GroupName = "IdentityService.PhoneNumberLogin";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PhoneNumberLoginPermissions));
        }

        public static class UserLookup
        {
            public const string Default = GroupName + ".UserLookup";
        }
    }
}