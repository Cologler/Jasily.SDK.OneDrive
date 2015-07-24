using System;
using Jasily.SDK.OneDrive.Authentications;
using System.Net;
using Jasily.SDK.OneDrive.OneDriveEntities;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

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

        protected HttpWebRequest GetRequest(Uri uri)
        {
            var request = WebRequest.CreateHttp(uri);
            request.Headers[HttpRequestHeader.Authorization] = $"bearer {this.AccessToken}";
            return request;
        }

        /// <summary>
        /// path was url after 'https://api.onedrive.com/v1.0/'
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal async Task<WebResult<T>> GetPathAsync<T>(string path)
        {
            var request = this.GetRequest(new Uri(RootApiUrl + path, UriKind.Absolute));
            return (await request.GetResultAsBytesAsync()).AsJson<T>();
        }

        public async Task<WebResult<Drive>> GetPrimaryDriveAsync()
        {
            return await this.GetPathAsync<Drive>("drive");
        }

        public async Task<WebResult<OneDriveArray<Drive>>> GetDrivesAsync()
        {
            return await this.GetPathAsync<OneDriveArray<Drive>>("drives");
        }
    }
}