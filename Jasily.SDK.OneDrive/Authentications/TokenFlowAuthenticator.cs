using System;
using System.Net;

namespace Jasily.SDK.OneDrive.Authentications
{
    public sealed class TokenFlowAuthenticator : Authenticator
    {
        public TokenFlowAuthenticator(string clientId)
            : base(clientId)
        {
        }

        public override Uri BuildAuthenticateUri(AuthenticationPermissions scope)
        {
            return base.BuildAuthenticateUri(scope, ResponseType.Token);

            // return value contain 'access_token' 'authentication_token' 'token_type' 'expires_in' 'scope' 'user_id'
        }

        public ITokenFlowAuthenticationInfo TryAuthenticateWithRedirectUri(string redirectUri)
        {
            if (!this.IsRedirectUriVaild(redirectUri))
                return null;

            // 'access_token' 'authentication_token' 'token_type' 'expires_in' 'scope' 'user_id'
            var builder = new HttpUriBuilder(redirectUri);
            var result = new AuthenticationInfo();
            foreach (var kvp in builder.QueryStringParameters)
            {
                switch (kvp.Key)
                {
                    case "access_token":
                        result.AccessToken = kvp.Value;
                        break;
                    case "token_type":
                        result.TokenType = kvp.Value;
                        break;
                    case "expires_in":
                        int i;
                        if (int.TryParse(kvp.Value, out i))
                            result.ExpiresIn = i;
                        break;
                    case "scope":
                        result.Scope = kvp.Value;
                        break;
                    case "authentication_token":
                        result.AuthenticationToken = kvp.Value;
                        break;
                    case "user_id":
                        result.UserId = kvp.Value;
                        break;

                    default:
                        break;
                }
            }

            return result.AccessToken.IsNullOrWhiteSpace() ? null : result;
        }
    }
}