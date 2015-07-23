using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Authentications
{
    [DataContract]
    internal sealed class AuthenticationInfo : ICodeFlowAuthenticationInfo, ITokenFlowAuthenticationInfo
    {
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// seconds
        /// </summary>
        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        public string AuthenticationToken { get; set; }

        public string UserId { get; set; }
    }
}