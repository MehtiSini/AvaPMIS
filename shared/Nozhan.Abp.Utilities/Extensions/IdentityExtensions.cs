﻿using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace Nozhan.Abp.Utilities.Extensions
{

    public static class IdentityExtensions
    {


        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindFirst(claimType)?.Value;
        }

        public static string GetUserClaimValue(this IIdentity identity, string claimType)
        {
            var identity1 = identity as ClaimsIdentity;
            return identity1?.FindFirstValue(claimType);
        }

        public static string GetUserFirstName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.GivenName);
        }

        public static T GetUserId<T>(this IIdentity identity)
        {
            var firstValue = identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
            return firstValue != null
                ? (T)Convert.ChangeType(firstValue, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public static string GetUserId(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserLastName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Surname);
        }

        public static string GetUserFullName(this IIdentity identity)
        {
            return $"{GetUserFirstName(identity)} {GetUserLastName(identity)}";
        }

        public static string GetUserDisplayName(this IIdentity identity)
        {
            var fullName = GetUserFullName(identity);
            return string.IsNullOrWhiteSpace(fullName) ? GetUserName(identity) : fullName;
        }

        public static string GetUserName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Name);
        }
    }
}