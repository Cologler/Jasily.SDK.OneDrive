using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class OneDriveArray<T> : OneDriveEntity
    {
        [DataMember(Name = "value")]
        public List<T> Value { get; set; }

        internal override void SetCreatorController(OneDriveWebController controller)
        {
            base.SetCreatorController(controller);

            if (this.Value != null)
            {
                foreach (var item in this.Value.OfType<OneDriveEntity>())
                {
                    item.SetCreatorController(controller);
                }
            }
        }
    }
}
