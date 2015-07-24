using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class Owner
    {
        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}