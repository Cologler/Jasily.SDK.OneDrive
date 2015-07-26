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
    public class OneDriveArray<T> : OneDriveEntity
    {
        [DataMember(Name = "value")]
        public List<T> Value { get; set; }

        [DataMember(Name = "@odata.nextLink")]
        public string NextPageUrl { get; set; }

        public bool HasNext() => !this.NextPageUrl.IsNullOrWhiteSpace();

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

        /// <summary>
        /// if controller is null, use CreatorController
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public async Task<WebResult<OneDriveArray<T>>> GetNextAsync(OneDriveWebController controller = null)
        {
            if (!this.HasNext())
                throw new InvalidOperationException("can not get next page.");

            return await (controller ?? this.CreatorController)
                .RawGetAsync<OneDriveArray<T>>(this.NextPageUrl);
        }
    }
}
