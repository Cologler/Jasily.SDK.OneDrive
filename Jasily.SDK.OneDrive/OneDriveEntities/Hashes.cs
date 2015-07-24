using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class Hashes
    {
        [DataMember(Name = "crc32Hash")]
        public string Crc32 { get; set; }

        [DataMember(Name = "sha1Hash")]
        public string Sha1 { get; set; }
    }
}