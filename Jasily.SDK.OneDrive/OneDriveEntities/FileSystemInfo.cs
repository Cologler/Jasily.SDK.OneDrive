using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class FileSystemInfo : OneDriveEntity
    {
        [DataMember(Name = "createdDateTime")]
        private string CreatedDateTime;

        [DataMember(Name = "lastModifiedDateTime")]
        public string LastModifiedDateTime;

        [IgnoreDataMember]
        public DateTime Created
        {
            get { return GetISODateTime(CreatedDateTime); }
        }

        [IgnoreDataMember]
        public DateTime Modified
        {
            get { return GetISODateTime(LastModifiedDateTime); }
        }
    }
}