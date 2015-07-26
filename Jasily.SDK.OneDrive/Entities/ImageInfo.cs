using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class ImageInfo
    {
        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }
    }
}