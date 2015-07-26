using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public abstract class OneDriveEntity : OneDriveObject
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
