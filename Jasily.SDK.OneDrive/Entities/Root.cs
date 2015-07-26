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
        /// if controller is null, use CreatorController
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public async Task<WebResult<OneDriveArray<Item>>> ListChildrenAsync(OneDriveWebController controller = null)
        {
            return await (controller ?? this.CreatorController)
                .WrapGetAsync<OneDriveArray<Item>>($"drive/items/{this.Id}/children");
        }
    }
}