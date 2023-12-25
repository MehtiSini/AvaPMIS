using Volo.Abp.Reflection;

namespace AvaPMIS.IdentityService.Identity
{
    public static class IdentityPermissionsConsts
    {
        public const string GroupName = "IdentityService";

        public static class Roles
        {
            public const string Admin = "Admins";
            public const string Operator = "Operators";
        }


        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityPermissionsConsts));
        }
    }
}