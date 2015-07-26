using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class IdentitySet
    {
        [DataMember(Name = "user")]
        public Identity User { get; set; }

        [DataMember(Name = "application")]
        public Identity Application { get; set; }

        [DataMember(Name = "device")]
        public Identity Device { get; set; }

        [DataMember(Name = "OneDriveSync")]
        public Identity OneDriveSync { get; set; }
    }
}