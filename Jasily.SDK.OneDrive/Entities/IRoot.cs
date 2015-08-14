using System.Threading.Tasks;
using Jasily.Net;

namespace Jasily.SDK.OneDrive.Entities
{
    public interface IRoot
    {
        IdentitySet CreatedBy { get; }
        string CTag { get; }
        string ETag { get; }
        FileSystemInfo FileSystemInfo { get; }
        FolderInfo FolderInfo { get; }
        string Id { get; }
        IdentitySet LastModifiedBy { get; }
        string Name { get; }
        long Size { get; }
        string WebUrl { get; }

        Task<WebResult<OneDriveItemPage<Item>>> ListChildrenAsync(OneDriveWebController controller = null, int? pageSize = default(int?));
    }
}