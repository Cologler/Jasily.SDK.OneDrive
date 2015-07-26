using System;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class Item : Root
    {
        [DataMember(Name = "@content.downloadUrl")]
        public string DownloadUrl { get; set; }

        [DataMember(Name = "parentReference")]
        public ItemReference Parent { get; set; }

        #region only file

        /// <summary>
        /// can be null. check before use.
        /// </summary>
        [DataMember(Name = "file")]
        public FileInfo FileInfo { get; set; }

        public bool IsFile() => this.FileInfo != null;

        #region only video

        /// <summary>
        /// only video contain this property
        /// </summary>
        [DataMember(Name = "video")]
        public VideoInfo VideoInfo { get; set; }

        #endregion

        #region only image

        /// <summary>
        /// can be null. check before use.
        /// </summary>
        [DataMember(Name = "image")]
        public ImageInfo ImageInfo { get; set; }

        /// <summary>
        /// can be null. check before use.
        /// </summary>
        [DataMember(Name = "photo")]
        public PhotoInfo PhotoInfo { get; set; }

        #endregion

        #endregion

        public string GetDownloadUrlWithName()
        {
            if (this.DownloadUrl.IsNullOrWhiteSpace())
                throw new NotSupportedException($"{nameof(this.DownloadUrl)} can not be empty.");

            return $"{this.DownloadUrl}/{System.Net.WebUtility.UrlEncode(this.Name)}";
        }

        public async Task<WebResult<OneDriveArray<ThumbnailSet>>> GetThumbnailsAsync(OneDriveWebController controller = null)
        {
            return await (controller ?? this.CreatorController)
                .WrapGetAsync<OneDriveArray<ThumbnailSet>>($"drive/items/{this.Id}/thumbnails");
        }

        public async Task<WebResult<string>> GetStreamUrlAsync(OneDriveWebController controller = null)
        {
            try
            {
                var request = (controller ?? this.CreatorController).CreateRequest($"drive/items/{this.Id}/content");
                var response = await request.GetResponseAsync();
                return new WebResult<string>(response, ((dynamic)request).Address.ToString());
            }
            catch (WebException e)
            {
                return new WebResult<string>(e);
            }
            catch (Exception e)
            {
                throw new NotSupportedException("can not get Address", e);
            }
        }
    }
}