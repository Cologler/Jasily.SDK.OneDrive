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

        public async Task<WebResult<OneDriveArray<Thumbnail>>> GetThumbnailsAsync(OneDriveWebController controller)
        {
            return await controller.RawGetAsync<OneDriveArray<Thumbnail>>($"/drive/items/{this.Id}/thumbnails");
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
    public class ThumbnailInfo : ImageInfo
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

    [DataContract]
    public class ImageInfo
    {
        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }
    }

    [DataContract]
    public class PhotoInfo : OneDriveObject
    {
        [DataMember(Name = "cameraMake")]
        public string CameraMake { get; set; }

        [DataMember(Name = "cameraModel")]
        public string CameraModel { get; set; }

        [DataMember(Name = "exposureDenominator")]
        public double ExposureDenominator { get; set; }

        [DataMember(Name = "exposureNumerator")]
        public double ExposureNumerator { get; set; }

        [DataMember(Name = "focalLength")]
        public double FocalLength { get; set; }

        [DataMember(Name = "fNumber")]
        public double FNumber { get; set; }

        [DataMember(Name = "takenDateTime")]
        private string TakenDateTime;

        [IgnoreDataMember]
        public DateTime Taken
        {
            get { return GetISODateTime(this.TakenDateTime); }
        }
    }

    [DataContract]
    public abstract class MediaInfo
    {
        [DataMember(Name = "bitrate")]
        public long BitRate { get; set; }

        [DataMember(Name = "duration")]
        public long Duration { get; set; }
    }

    [DataContract]
    public class AudioInfo : MediaInfo
    {
        [DataMember(Name = "album")]
        public string Album { get; set; }

        [DataMember(Name = "albumArtist")]
        public string AlbumArtist { get; set; }

        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        [DataMember(Name = "composers")]
        public string Composers { get; set; }

        [DataMember(Name = "copyright")]
        public string Copyright { get; set; }

        [DataMember(Name = "disc")]
        public short Disc { get; set; }

        [DataMember(Name = "discCount")]
        public short DiscCount { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

        [DataMember(Name = "hasDrm")]
        public bool HasDRM { get; set; }

        [DataMember(Name = "isVariableBitrate")]
        public bool IsVariableBitRate { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "track")]
        public int Track { get; set; }

        [DataMember(Name = "trackCount")]
        public int TrackCount { get; set; }

        [DataMember(Name = "year")]
        public int Year { get; set; }


    }
}