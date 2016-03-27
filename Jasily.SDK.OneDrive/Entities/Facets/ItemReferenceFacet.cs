using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities.Facets
{
    [DataContract]
    public class ItemReferenceFacet : ItemReferenceFacetOnlyId
    {
        [DataMember(Name = "driveId")]
        public string DriveId { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }
    }
}