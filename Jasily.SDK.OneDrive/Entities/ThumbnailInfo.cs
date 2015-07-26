using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class ThumbnailInfo : ImageInfo
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}