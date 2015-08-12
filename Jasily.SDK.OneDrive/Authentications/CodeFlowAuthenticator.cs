using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Jasily.Net;

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

        public async Task<ICodeFlowAuthenticationInfo> TryAuthenticateWithRedirectUri(string redirectUri)
        {
            var start = RedirectUri + "?code=";
            if (!redirectUri.StartsWith(start))
                return null;
            var builder = new HttpUriBuilder(redirectUri);
            var code = builder.QueryStringParameters.FirstOrDefault(z => z.Key == "code");
            if (code.Value.IsNullOrWhiteSpace())
                return null;

            var body =
                $"client_id={this.ClientId}&redirect_uri={RedirectUri}&client_secret={this.ClientSecret}&code={code.Value}&grant_type=authorization_code";
            var result = await RedeemAccessTokenAsync(body);
            return result.Result;
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

            private int UpdatedCount;

            public string ClientId { get; }
            public string ClientSecret { get; }
            public string RefreshToken { get; }

            public bool Updating { get; private set; }

            public string LastAccessToken { get; private set; }
            public DateTime LastUpdateTime { get; private set; }
            public int LastExpiresIn { get; private set; }

            public event EventHandler<bool> AccessTokenUpdateResult;
            public event EventHandler<string> AccessTokenUpdated;

            public async Task<bool> UpdateTokenAsync()
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
                    this.UpdatedCount++;
                    this.LastAccessToken = result.Result.AccessToken;
                    this.LastUpdateTime = DateTime.UtcNow;
                    this.LastExpiresIn = result.Result.ExpiresIn;

                    this.AccessTokenUpdated.BeginFire(this, result.Result.AccessToken);
                    return true;
                }
                this.AccessTokenUpdateResult.BeginFire(this, result.IsSuccess);

                return false;
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

                var version = this.UpdatedCount;
                var count = 0;
                while (this.Updating && count < 3)
                {
                    count++;
                    if (version != this.UpdatedCount || await this.UpdateTokenAsync())
                    {
                        version = this.UpdatedCount;
                        count = 0;

                        var time = this.LastExpiresIn - (DateTime.UtcNow - LastUpdateTime).TotalSeconds;
                        if (time > 60) await Task.Delay((int)time * 500);
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