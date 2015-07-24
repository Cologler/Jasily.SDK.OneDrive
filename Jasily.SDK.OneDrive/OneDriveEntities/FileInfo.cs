using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class FileInfo
    {
        [DataMember(Name = "hashes")]
        public Hashes Hashes { get; set; }

        [DataMember(Name = "mimeType")]
        public string MimeType { get; set; }
    }
}