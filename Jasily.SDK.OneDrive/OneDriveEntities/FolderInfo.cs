using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class FolderInfo
    {
        [DataMember(Name = "childCount")]
        public long ChildCount { get; set; }
    }
}