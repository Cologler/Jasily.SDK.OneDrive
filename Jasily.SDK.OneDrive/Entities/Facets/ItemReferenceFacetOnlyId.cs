using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities.Facets
{
    [DataContract]
    public class ItemReferenceFacetOnlyId
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}