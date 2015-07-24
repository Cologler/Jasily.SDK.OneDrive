using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class VideoMeta
    {
        [DataMember(Name = "bitrate")]
        public long BitRate { get; set; }

        [DataMember(Name = "duration")]
        public long Duration { get; set; }

        [DataMember(Name = "height")]
        public long Height { get; set; }

        [DataMember(Name = "width")]
        public long Width { get; set; }
    }
}