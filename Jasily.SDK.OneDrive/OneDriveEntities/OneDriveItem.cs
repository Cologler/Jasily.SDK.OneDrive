using System.Runtime.Serialization;

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
    }
}