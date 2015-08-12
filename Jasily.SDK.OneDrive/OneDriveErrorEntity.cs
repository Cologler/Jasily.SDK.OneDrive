using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive
{
    [DataContract]
    public sealed class OneDriveErrorEntity
    {
        [DataMember(Name = "error")]
        public Error Error { get; set; }
    }
}