using System;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class Root : FileSystemInfo
    {
        [DataMember(Name = "createdBy")]
        public IdentitySet CreatedBy { get; set; }

        [DataMember(Name = "cTag")]
        public string CTag { get; set; }

        [DataMember(Name = "eTag")]
        public string ETag { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "lastModifiedBy")]
        public IdentitySet LastModifiedBy { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "size")]
        public long Size { get; set; }

        [DataMember(Name = "webUrl")]
        public string WebUrl { get; set; }

        [DataMember(Name = "fileSystemInfo")]
        public FileSystemInfo FileSystemInfo { get; set; }

        #region only folder

        /// <summary>
        /// can be null. check before use.
        /// </summary>
        [DataMember(Name = "folder")]
        public FolderInfo FolderInfo { get; set; }

        #endregion

        /// <summary>
        /// if controller is null, use CreatorController.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="pageSize">default value was 200, max value was 1000 ( test on 2015-07-27 ).</param>
        /// <returns></returns>
        public async Task<WebResult<OneDriveItemPage<Item>>> ListChildrenAsync(OneDriveWebController controller = null, int? pageSize = null)
        {
            if (pageSize.HasValue)
            {
                if (pageSize.Value <= 0 || pageSize.Value > 1000)
                    throw new ArgumentOutOfRangeException($"{nameof(pageSize)} must be 0 < {nameof(pageSize)} < 1001");

                return await (controller ?? this.CreatorController)
                    .WrapRequestAsync<OneDriveItemPage<Item>>($"drive/items/{this.Id}/children?top={pageSize}");
            }
            else
            {
                return await (controller ?? this.CreatorController)
                    .WrapRequestAsync<OneDriveItemPage<Item>>($"drive/items/{this.Id}/children");
            }
        }
    }
}