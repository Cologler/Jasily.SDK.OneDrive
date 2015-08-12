using System;
using System.Net;
using System.Threading.Tasks;
using Jasily.Net;

namespace Jasily.SDK.OneDrive.Authentications
{
    public abstract class Authenticator
    {
        protected const string AuthenticationUrl = "https://login.live.com/oauth20_authorize.srf";
        public const string RedirectUri = "https://login.live.com/oauth20_desktop.srf";

        protected Authenticator(string clientId)
        {
            this.ClientId = clientId;
        }

        public string ClientId { get; }

        public abstract Uri BuildAuthenticateUri(AuthenticationPermissions scope);

        protected enum ResponseType
        {
            Token, Code
        }

        protected Uri BuildAuthenticateUri(AuthenticationPermissions scope, ResponseType responseType)
        {
            var builder = new HttpUriBuilder(AuthenticationUrl);
            builder.AddQueryStringParameter("client_id", this.ClientId);
            builder.AddQueryStringParameter("response_type", responseType.ToString().ToLower());
            builder.AddQueryStringParameter("scope", scope.GetParameterValue());
            builder.AddQueryStringParameter("redirect_uri", RedirectUri);
            return builder.Build();
        }

        public bool IsRedirectUriVaild(string redirectUri)
        {
            return redirectUri.StartsWith(RedirectUri);
        }

        public async Task<bool> LogoutAsync()
        {
            var url = $"https://login.live.com/oauth20_logout.srf?client_id={this.ClientId}&redirect_uri={RedirectUri}";
            var request = WebRequest.CreateHttp(url);
            return (await request.GetResultAsync()).IsSuccess;
        }        
    }
}