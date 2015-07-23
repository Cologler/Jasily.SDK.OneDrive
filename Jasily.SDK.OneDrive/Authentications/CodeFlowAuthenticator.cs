using System;
using System.Net;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.Authentications
{
    public sealed class CodeFlowAuthenticator : Authenticator
    {
        public string ClientSecret { get; }

        public CodeFlowAuthenticator(string clientId, string clientSecret)
            : base(clientId)
        {
            this.ClientSecret = clientSecret;
        }

        public override Uri BuildAuthenticateUri(AuthenticationPermissions scope)
        {
            return base.BuildAuthenticateUri(scope, ResponseType.Code);
        }

        public async Task<bool> TryAuthenticateWithRedirectUri(string redirectUri)
        {
            var start = RedirectUri + "?code=";
            if (!redirectUri.StartsWith(start))
                return false;
            var code = redirectUri.Substring(start.Length);
            if (code.IsNullOrWhiteSpace())
                return false;

            var body =
                $"client_id={this.ClientId}&redirect_uri={RedirectUri}&client_secret={this.ClientSecret}&code={code}&grant_type=authorization_code";
            var result = await RedeemAccessTokenAsync(body);
            if (result.IsSuccess)
            {
		        
            }

            return true;
        }

        private static async Task<WebResult<AuthenticationInfo>> RedeemAccessTokenAsync(string bodyString)
        {
            if (bodyString == null) throw new ArgumentNullException(nameof(bodyString));

            // do some thing
            var request = WebRequest.CreateHttp("https://login.live.com/oauth20_token.srf");
            request.Method = HttpWebRequestResourceString.Method.Post;
            request.ContentType = HttpWebRequestResourceString.ContentType.ApplicationXWwwFormUrlencoded;
            return (await request.SendAndGetResultAsBytesAsync(bodyString.GetBytes().ToMemoryStream())).AsJson<AuthenticationInfo>();
        }

        public sealed class TokenWatcher
        {
            private readonly object syncRoot = new object();

            public string ClientId { get; }
            public string ClientSecret { get; }
            public string RefreshToken { get; }

            public bool Updating { get; private set; }

            public string LastAccessToken { get; private set; }
            public DateTime LastUpdateTime { get; private set; }
            public int LastExpiresIn { get; private set; }

            public event EventHandler<string> AccessTokenUpdated;

            private async Task<int> UpdateTokenAsync()
            {
                var body = string.Format(
                    "client_id={0}&redirect_uri={1}&client_secret={2}&refresh_token={3}&grant_type=refresh_token",
                    this.ClientId,
                    RedirectUri,
                    this.ClientSecret,
                    this.RefreshToken);

                var result = (await RedeemAccessTokenAsync(body));
                if (result.IsSuccess)
                {
                    this.LastAccessToken = result.Result.AccessToken;
                    this.LastUpdateTime = DateTime.UtcNow;
                    this.LastExpiresIn = result.Result.ExpiresIn;

                    this.AccessTokenUpdated.BeginFire(this, result.Result.AccessToken);
                    return result.Result.ExpiresIn;
                }

                return -1;
            }

            public TokenWatcher(string clientId, string clientSecret, string refreshToken)
            {
                if (refreshToken.IsNullOrWhiteSpace())
                    throw new ArgumentNullException(nameof(refreshToken));

                this.ClientId = clientId;
                this.ClientSecret = clientSecret;
                this.RefreshToken = refreshToken;
            }

            public async void Start()
            {
                this.Updating = true;

                var count = 0;
                var time = -1;
                while (this.Updating && count < 3)
                {
                    count++;
                    if ((time = await this.UpdateTokenAsync()) > 0)
                    {
                        count = 0;
                        await Task.Delay(time * 1000 / 2);
                    }
                }
            }

            public void Stop()
            {
                this.Updating = false;
            }
        }

        
    }
}