using System;
using Jasily.SDK.OneDrive.Authentications;
using System.Net;
using Jasily.SDK.OneDrive.Entities;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Jasily.Net;

namespace Jasily.SDK.OneDrive
{
    public class OneDriveWebController
    {
        private CodeFlowAuthenticator.TokenWatcher tokenWatcher;

        /// <summary>
        /// return 'https://api.onedrive.com/v1.0'
        /// </summary>
        public const string RootApiUrl = "https://api.onedrive.com/v1.0/";

        public CodeFlowAuthenticator.TokenWatcher TokenWatcher
        {
            get { return this.tokenWatcher; }
            set
            {
                if (this.tokenWatcher != value)
                {
                    if (this.tokenWatcher != null)
                    {
                        this.tokenWatcher.AccessTokenUpdated -= this.TokenWatcher_AccessTokenUpdated;
                    }

                    if (value != null)
                    {
                        value.AccessTokenUpdated += this.TokenWatcher_AccessTokenUpdated;
                    }

                    this.tokenWatcher = value;
                }
            }
        }

        public string AccessToken { get; private set; }
        
        public OneDriveWebController(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        private void TokenWatcher_AccessTokenUpdated(object sender, string e)
        {
            this.AccessToken = e;
        }

        internal HttpWebRequest CreateRequest(string url)
        {
            var request = WebRequest.CreateHttp(url);
            request.Headers[HttpRequestHeader.Authorization] = $"bearer {this.AccessToken}";
            return request;
        }

        internal async Task<WebResult<T>> RawRequestAsync<T>(string url, string method = HttpWebRequestResourceString.Method.Get, byte[] body = null)
            where T : OneDriveEntity
        {
            return await Task.Run(async () =>
            {
                var request = this.CreateRequest(url);
                request.Method = method;
                if (body != null)
                    request.ContentType = HttpWebRequestResourceString.ContentType.Application.Json;
                var bytesResult = body == null
                    ? await request.GetResultAsBytesAsync()
                    : await request.SendAndGetResultAsBytesAsync(body.ToMemoryStream());

                var result = bytesResult.AsJson<T>();
                if (result.IsSuccess)
                    result.Result.SetCreatorController(this);
                return result;
            });
        }

        /// <summary>
        /// path was url after 'https://api.onedrive.com/v1.0/'
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        internal async Task<WebResult<T>> WrapRequestAsync<T>(string path, string method = HttpWebRequestResourceString.Method.Get, byte[] body = null)
            where T : OneDriveEntity
        {
            return await this.RawRequestAsync<T>(RootApiUrl + path, method, body);
        }

        public async Task<WebResult<Drive>> GetPrimaryDriveAsync()
        {
            return await this.WrapRequestAsync<Drive>("drive");
        }

        public async Task<WebResult<OneDriveItemPage<Drive>>> GetDrivesAsync()
        {
            return await this.WrapRequestAsync<OneDriveItemPage<Drive>>("drives");
        }
    }
}