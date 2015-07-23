using System;

namespace Jasily.SDK.OneDrive.Authentications
{
    [Flags]
    public enum AuthenticationPermissions
    {
        /// <summary>
        /// can not use this.
        /// </summary>
        None = 0,

        /// <summary>
        /// Allows your application to take advantage of single sign-on capabilities.
        /// </summary>
        WindowsLiveSignIn = 1,

        /// <summary>
        /// Allows your application to receive a refresh token so it can work offline even when the user isn't active. This scope is not available for token flow.
        /// </summary>
        WindowsLiveOfflineAccess = 2,

        /// <summary>
        /// Grants read-only permission to all of a user's OneDrive files, including files shared with the user.
        /// </summary>
        OnedriveReadonly = 4,

        /// <summary>
        /// Grants read and write permission to all of a user's OneDrive files, including files shared with the user. To create sharing links, this scope is required.
        /// </summary>
        OnedriveReadwrite = 8,

        /// <summary>
        /// Grants read and write permissions to a specific folder for your application.
        /// </summary>
        OnedriveAppfolder = 16
    }
}