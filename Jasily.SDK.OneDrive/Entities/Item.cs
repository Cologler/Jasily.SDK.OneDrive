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

        public async Task<WebResult<OneDriveArray<ThumbnailSet>>> GetThumbnailsAsync(OneDriveWebController controller)
        {
            return await controller.RawGetAsync<OneDriveArray<ThumbnailSet>>($"/drive/items/{this.Id}/thumbnails");
        }

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
    }
}