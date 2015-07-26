using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class Quota
    {
        [DataMember(Name = "deleted")]
        public long Deleted { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "total")]
        public long Total { get; set; }

        [DataMember(Name = "used")]
        public long Used { get; set; }

        [DataMember(Name = "remaining")]
        public long Remaining { get; set; }
    }
}