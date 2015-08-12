using System.IO;
using System.Runtime.Serialization.Json;
using Jasily.Net;

namespace Jasily.SDK.OneDrive
{
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
}