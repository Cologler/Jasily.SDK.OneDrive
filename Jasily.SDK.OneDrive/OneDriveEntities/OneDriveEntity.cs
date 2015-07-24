using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class OneDriveEntity
    {
        [DataMember(Name = "@odata.context")]
        public string ODataContext { get; set; }

        [IgnoreDataMember]
        public OneDriveWebController CreatorController { get; private set; }

        public DateTime GetISODateTime(string value)
        {
            return value.Length == 24
                ? DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'", CultureInfo.InvariantCulture)
                : DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ff'Z'", CultureInfo.InvariantCulture);
        }

        internal virtual void SetCreatorController(OneDriveWebController controller)
        {
            this.CreatorController = controller;
        }
    }
}
