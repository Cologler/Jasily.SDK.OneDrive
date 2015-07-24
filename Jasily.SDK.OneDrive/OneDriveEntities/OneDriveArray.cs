using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class OneDriveArray<T> : OneDriveEntity
    {
        [DataMember(Name = "value")]
        public List<T> Value { get; set; }
    }
}
