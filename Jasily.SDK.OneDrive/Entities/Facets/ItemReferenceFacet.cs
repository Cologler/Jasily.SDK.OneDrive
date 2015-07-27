using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities.Facets
{
    [DataContract]
    public class ItemReferenceFacet
    {
        [DataMember(Name = "driveId")]
        public string DriveId { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }
    }
}