using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class FolderInfo
    {
        [DataMember(Name = "childCount")]
        public long ChildCount { get; set; }
    }
}