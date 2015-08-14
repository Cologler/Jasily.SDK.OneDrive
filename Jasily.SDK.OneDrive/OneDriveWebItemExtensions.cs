using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Jasily.Net;
using Jasily.SDK.OneDrive.Entities;
using Jasily.SDK.OneDrive.Options;

namespace Jasily.SDK.OneDrive
{
    public static class OneDriveWebItemExtensions
    {
        /// <summary>
        /// if controller is null, use CreatorController.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="controller"></param>
        /// <param name="pageSize">default value was 200, max value was 1000 ( test on 2015-07-27 ).</param>
        /// <returns></returns>
        public async static Task<WebResult<OneDriveItemPage<Item>>> ListChildrenAsync(this IRoot folder, OneDriveWebController controller = null, int? pageSize = null)
        {
            if (pageSize.HasValue)
            {
                if (pageSize.Value <= 0 || pageSize.Value > 1000)
                    throw new ArgumentOutOfRangeException($"{nameof(pageSize)} must be 0 < {nameof(pageSize)} < 1001");

                return await (controller ?? folder.CreatorController)
                    .WrapRequestAsync<OneDriveItemPage<Item>>($"drive/items/{folder.Id}/children?top={pageSize}");
            }
            else
            {
                return await (controller ?? folder.CreatorController)
                    .WrapRequestAsync<OneDriveItemPage<Item>>($"drive/items/{folder.Id}/children");
            }
        }

        /// <summary>
        /// if controller is null, use CreatorController.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="newFolderName"></param>
        /// <param name="conflict"></param>
        /// <param name="controller"></param>
        /// <param name="pageSize">default value was 200, max value was 1000 ( test on 2015-07-27 ).</param>
        /// <returns></returns>
        public async static Task<WebResult<Item>> CreateFolderAsync(this IRoot folder,
            string newFolderName, ConflictBehavior conflict = ConflictBehavior.Fail, OneDriveWebController controller = null)
        {
            var entity = new CreateFolderEntity()
            {
                Name = newFolderName,
                NameconflictBehavior = conflict.GetValue()
            };

            var json = entity.ObjectToJson();
            Debug.WriteLine(json);

            return await (controller ?? folder.CreatorController)
                    .WrapRequestAsync<Item>($"drive/items/{folder.Id}/children",
                    HttpWebRequestResourceString.Method.Post,
                    json.GetBytes());
        }

        private class CreateFolderEntity
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "folder")]
            public EmptyEntity Folder { get; set; }

            [DataMember(Name = "nameconflictBehavior")]
            public string NameconflictBehavior { get; set; }
        }
    }
}