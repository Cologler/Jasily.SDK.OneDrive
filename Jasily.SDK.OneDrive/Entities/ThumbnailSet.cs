using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class ThumbnailSet
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "small")]
        public ThumbnailInfo Small { get; set; }

        [DataMember(Name = "medium")]
        public ThumbnailInfo Medium { get; set; }

        [DataMember(Name = "large")]
        public ThumbnailInfo Large { get; set; }

        /// <summary>
        /// To determine if a custom uploaded thumbnail exists on a file, 
        /// look for the source property on the thumbnail set. 
        /// If it has a value, 
        /// then the value represents the custom uploaded thumbnail. 
        /// If it is not present, then no custom uploaded thumbnail exists.
        /// </summary>
        [DataMember(Name = "source")]
        public ThumbnailInfo Custom { get; set; }
    }
}