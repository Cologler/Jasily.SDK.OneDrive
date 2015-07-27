using System;
using Jasily.SDK.OneDrive.Authentications;
using System.Net;
using Jasily.SDK.OneDrive.Entities;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

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

        internal async Task<WebResult<T>> RawRequestAsync<T>(string url, string method = HttpWebRequestResourceString.Method.Get, byte[] body = null)
            where T : OneDriveEntity
        {
            return await Task.Run(async () =>
            {
                var request = this.CreateRequest(url);
                request.Method = method;
                if (body != null)
                    request.ContentType = HttpWebRequestResourceString.ContentType.ApplicationJson;
                var bytesResult = body == null
                    ? await request.GetResultAsBytesAsync()
                    : await request.SendAndGetResultAsBytesAsync(body.ToMemoryStream());

                // print
                if (bytesResult.IsSuccess)
                {
                    Debug.WriteLine(bytesResult.Result.Length);
                }
                else if (bytesResult.Response != null && bytesResult.Response.ContentLength > 0)
                {
                    using (var stream = bytesResult.Response.GetResponseStream())
                        Debug.WriteLine(stream.ToArray().GetString());
                }

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

    public static class OneDriveErrorExtensions
    {
        public static OneDriveErrorEntity TryGetErrorEntity<T>(this WebResult<T> result)
        {
            if (!result.IsSuccess && result.Response != null && result.Response.ContentLength > 0)
            {
                using (var stream = result.Response.GetResponseStream())
                    return stream.ToArray().JsonToObject<OneDriveErrorEntity>();
            }

            return null;
        }
    }

    [DataContract]
    public sealed class OneDriveErrorEntity
    {
        [DataMember(Name = "error")]
        public Error Error { get; set; }
    }

    [DataContract]
    public class Error
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }

}