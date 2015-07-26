using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class VideoInfo : MediaInfo
    {
        [DataMember(Name = "height")]
        public long Height { get; set; }

        [DataMember(Name = "width")]
        public long Width { get; set; }
    }
}