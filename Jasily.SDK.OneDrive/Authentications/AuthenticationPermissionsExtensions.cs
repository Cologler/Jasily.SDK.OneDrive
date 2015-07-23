using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasily.SDK.OneDrive.Authentications
{
    public static class AuthenticationPermissionsExtensions
    {
        public static string GetString(this AuthenticationPermissions permission)
        {
            switch (permission)
            {
                case AuthenticationPermissions.WindowsLiveSignIn:
                    return "wl.signin";
                case AuthenticationPermissions.WindowsLiveOfflineAccess:
                    return "wl.offline_access";
                case AuthenticationPermissions.OnedriveReadonly:
                    return "onedrive.readonly";
                case AuthenticationPermissions.OnedriveReadwrite:
                    return "onedrive.readwrite";
                case AuthenticationPermissions.OnedriveAppfolder:
                    return "onedrive.appfolder";

                case AuthenticationPermissions.None:
                default:
                    throw new ArgumentOutOfRangeException(nameof(permission), permission, null);
            }
        }

        /// <summary>
        /// return like 'wl.signin wl.offline_access onedrive.readwrite'
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public static string GetParameterValue(AuthenticationPermissions permissions)
        {
            if (permissions == AuthenticationPermissions.None)
                throw new ArgumentException("can not use None.", nameof(permissions));

            return string.Join(" ", GetSubPermissions(permissions).Select(z => GetString(z)));
        }

        private static IEnumerable<AuthenticationPermissions> GetSubPermissions(AuthenticationPermissions permissions)
        {
            if (permissions.HasFlag(AuthenticationPermissions.WindowsLiveSignIn))
                yield return AuthenticationPermissions.WindowsLiveSignIn;
            if (permissions.HasFlag(AuthenticationPermissions.WindowsLiveOfflineAccess))
                yield return AuthenticationPermissions.WindowsLiveOfflineAccess;
            if (permissions.HasFlag(AuthenticationPermissions.OnedriveReadonly))
                yield return AuthenticationPermissions.OnedriveReadonly;
            if (permissions.HasFlag(AuthenticationPermissions.OnedriveReadwrite))
                yield return AuthenticationPermissions.OnedriveReadwrite;
            if (permissions.HasFlag(AuthenticationPermissions.OnedriveAppfolder))
                yield return AuthenticationPermissions.OnedriveAppfolder;
        }
    }
}