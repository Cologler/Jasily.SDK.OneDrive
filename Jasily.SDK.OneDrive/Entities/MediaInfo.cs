using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public abstract class MediaInfo
    {
        [DataMember(Name = "bitrate")]
        public long BitRate { get; set; }

        [DataMember(Name = "duration")]
        public long Duration { get; set; }
    }
}