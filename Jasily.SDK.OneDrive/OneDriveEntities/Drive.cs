using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.OneDriveEntities
{
    [DataContract]
    public class Drive : OneDriveEntity
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "driveType")]
        public string DriveType { get; set; }

        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        [DataMember(Name = "quota")]
        public Quota Quota { get; set; }
        
        public DriveType GetDriveType()
        {
            switch (this.DriveType)
            {
                case "personal":
                    return OneDriveEntities.DriveType.Personal;

                default:
                    throw new ArgumentException(nameof(this.DriveType));
            }
        }

        public async Task<WebResult<Root>> GetRootAsync(OneDriveWebController controller)
        {
            return await controller.RawGetAsync<Root>("drive/root");
        }
    }
}
