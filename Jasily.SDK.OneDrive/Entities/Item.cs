using System;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Jasily.SDK.OneDrive.Entities.Facets;
using System.Runtime.Serialization.Json;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public class Item : Root
    {
        [DataMember(Name = "@content.downloadUrl")]
        public string DownloadUrl { get; set; }

        [DataMember(Name = "parentReference")]
        public ItemReferenceFacet ParentInfo { get; set; }

        #region only file

        /// <summary>
        /// can be null. check before use.
        /// </summary>
        [DataMember(Name = "file")]
        public FileInfo FileInfo { get; set; }

        public bool IsFile() => this.FileInfo != null;

        #region only video

        /// <summary>
        /// only video contain this property
        /// </summary>
        [DataMember(Name = "video")]
        public VideoInfo VideoInfo { get; set; }

        #endregion

        #region only image

        /// <summary>
        /// can be null. check before use.
        /// </summary>
        [DataMember(Name = "image")]
        public ImageInfo ImageInfo { get; set; }

        /// <summary>
        /// can be null. check before use.
        /// </summary>
        [DataMember(Name = "photo")]
        public PhotoInfo PhotoInfo { get; set; }

        #endregion

        #endregion

        public string GetDownloadUrlWithName()
        {
            if (this.DownloadUrl.IsNullOrWhiteSpace())
                throw new NotSupportedException($"{nameof(this.DownloadUrl)} can not be empty.");

            return $"{this.DownloadUrl}/{System.Net.WebUtility.UrlEncode(this.Name)}";
        }

        public async Task<WebResult<OneDriveArray<ThumbnailSet>>> GetThumbnailsAsync(OneDriveWebController controller = null)
        {
            return await (controller ?? this.CreatorController)
                .WrapRequestAsync<OneDriveArray<ThumbnailSet>>($"drive/items/{this.Id}/thumbnails");
        }

        public async Task<WebResult<string>> GetStreamUrlAsync(OneDriveWebController controller = null)
        {
            try
            {
                var request = (controller ?? this.CreatorController).CreateRequest($"drive/items/{this.Id}/content");
                var response = await request.GetResponseAsync();
                return new WebResult<string>(response, ((dynamic)request).Address.ToString());
            }
            catch (WebException e)
            {
                return new WebResult<string>(e);
            }
            catch (Exception e)
            {
                throw new NotSupportedException("can not get Address", e);
            }
        }

        public async Task<WebResult<Item>> MoveByIdAsync(string targetFolderId, string newName, OneDriveWebController controller = null)
        {
            if (targetFolderId.IsNullOrWhiteSpace())
                throw new ArgumentException($"{nameof(targetFolderId)} can not be empty.");

            var entity = new MoveMethodBodyEntity()
            {
                Name = newName.IsNullOrWhiteSpace() ? this.Name : newName,
                ParentInfo = new ItemReferenceFacetOnlyId()
                {
                    Id = targetFolderId
                }
            };

            return await (controller ?? this.CreatorController).WrapRequestAsync<Item>(
                $"drive/items/{this.Id}/thumbnails",
                HttpWebRequestResourceString.Method.Patch,
                entity.ObjectToJson().GetBytes());
        }

        [DataContract]
        private class MoveMethodBodyEntity
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "parentReference")]
            public ItemReferenceFacetOnlyId ParentInfo { get; set; }
        }
    }


}