using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class Operators
    {
        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "application")]
        public User Application { get; set; }

        [DataMember(Name = "device")]
        public User Device { get; set; }

        [DataMember(Name = "OneDriveSync")]
        public User OneDriveSync { get; set; }
    }
}