using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
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