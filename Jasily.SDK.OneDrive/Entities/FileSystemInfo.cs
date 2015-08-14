using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class FileSystemInfo : OneDriveEntity
    {
        [DataMember(Name = "createdDateTime")]
        private string CreatedDateTime;

        [DataMember(Name = "lastModifiedDateTime")]
        public string LastModifiedDateTime;

        [IgnoreDataMember]
        public DateTime Created => this.GetISODateTime(this.CreatedDateTime);

        [IgnoreDataMember]
        public DateTime Modified => this.GetISODateTime(this.LastModifiedDateTime);
    }
}