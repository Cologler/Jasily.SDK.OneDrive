using System;
using Jasily.SDK.OneDrive.Authentications;
using System.Net;
using Jasily.SDK.OneDrive.Entities;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Diagnostics;

namespace Jasily.SDK.OneDrive
{
    public class OneDriveWebController
    {
        static OneDriveWebController()
        {
        }

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
                if (tokenWatcher != value)
                {
                    if (tokenWatcher != null)
                    {
                        tokenWatcher.AccessTokenUpdated -= this.TokenWatcher_AccessTokenUpdated;
                    }

                    if (value != null)
                    {
                        value.AccessTokenUpdated += this.TokenWatcher_AccessTokenUpdated;
                    }

                    tokenWatcher = value;
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

        internal async Task<WebResult<T>> RawGetAsync<T>(string url)
            where T : OneDriveEntity
        {
            var request = this.CreateRequest(url);
            request.Method = HttpWebRequestResourceString.Method.Get;
            var bytesResult = await request.GetResultAsBytesAsync();
            if (bytesResult.IsSuccess)
                Debug.WriteLine(bytesResult.Result.GetString());
            var result = bytesResult.AsJson<T>();
            if (result.IsSuccess)
                result.Result.SetCreatorController(this);
            return result;
        }

        /// <summary>
        /// path was url after 'https://api.onedrive.com/v1.0/'
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<WebResult<T>> WrapGetAsync<T>(string path)
            where T : OneDriveEntity
        {
            return await this.RawGetAsync<T>(RootApiUrl + path);
        }

        public async Task<WebResult<Drive>> GetPrimaryDriveAsync()
        {
            return await this.WrapGetAsync<Drive>("drive");
        }

        public async Task<WebResult<OneDriveArray<Drive>>> GetDrivesAsync()
        {
            return await this.WrapGetAsync<OneDriveArray<Drive>>("drives");
        }
    }
}