using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class OneDriveItem : Root
    {
        [DataMember(Name = "@content.downloadUrl")]
        public Parent DownloadUrl { get; set; }

        [DataMember(Name = "parentReference")]
        public Parent Parent { get; set; }

        [DataMember(Name = "file")]
        public FileInfo FileInfo { get; set; }

        [DataMember(Name = "video")]
        public VideoMeta VideoMeta { get; set; }

        public bool IsFile() => this.FileInfo != null;

        public async Task<WebResult<OneDriveArray<Thumbnail>>> GetThumbnailsAsync(OneDriveWebController controller)
        {
            return await controller.RawGetAsync<OneDriveArray<Thumbnail>>($"/drive/items/{this.Id}/thumbnails");
        }
    }

    [DataContract]
    public class Thumbnail
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "small")]
        public ThumbnailInfo Small { get; set; }

        [DataMember(Name = "medium")]
        public ThumbnailInfo Medium { get; set; }

        [DataMember(Name = "large")]
        public ThumbnailInfo Large { get; set; }

        /// <summary>
        /// To determine if a custom uploaded thumbnail exists on a file, 
        /// look for the source property on the thumbnail set. 
        /// If it has a value, 
        /// then the value represents the custom uploaded thumbnail. 
        /// If it is not present, then no custom uploaded thumbnail exists.
        /// </summary>
        [DataMember(Name = "source")]
        public ThumbnailInfo Custom { get; set; }
    }

    [DataContract]
    public class ThumbnailInfo
    {
        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}