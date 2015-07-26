using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.Entities
{
    public abstract class OneDriveObject
    {
        public DateTime GetISODateTime(string value)
        {
            switch (value.Length)
            {
                case 23:
                    return DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ff'Z'", CultureInfo.InvariantCulture);
                case 24:
                    return DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'", CultureInfo.InvariantCulture);
                default:
                    return DateTime.ParseExact(value, $"yyyy'-'MM'-'dd'T'HH':'mm':'ss.{'f'.Repeat(value.Length - 21)}'Z'", CultureInfo.InvariantCulture);
            }
        }
    }

    [DataContract]
    public class OneDriveEntity : OneDriveObject
    {
        [DataMember(Name = "@odata.context")]
        public string ODataContext { get; set; }

        [IgnoreDataMember]
        public OneDriveWebController CreatorController { get; private set; }

        internal virtual void SetCreatorController(OneDriveWebController controller)
        {
            this.CreatorController = controller;
        }
    }
}
