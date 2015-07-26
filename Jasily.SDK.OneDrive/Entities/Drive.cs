using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class Drive : OneDriveEntity
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "driveType")]
        public string DriveType { get; set; }

        [DataMember(Name = "owner")]
        public IdentitySet Owner { get; set; }

        [DataMember(Name = "quota")]
        public Quota Quota { get; set; }
        
        public DriveType GetDriveType()
        {
            switch (this.DriveType)
            {
                case "personal":
                    return Entities.DriveType.Personal;

                default:
                    throw new ArgumentException(nameof(this.DriveType));
            }
        }

        public async Task<WebResult<Root>> GetRootAsync(OneDriveWebController controller = null)
        {
            return await (controller ?? this.CreatorController)
                .WrapGetAsync<Root>("drive/root");
        }
    }
}
