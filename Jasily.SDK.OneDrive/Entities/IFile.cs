using System.Threading.Tasks;
using Jasily.Net;
using Jasily.SDK.OneDrive.Entities.Facets;

namespace Jasily.SDK.OneDrive.Entities
{
    public interface IFile : IItem
    {
        string DownloadUrl { get; }
        FileInfo FileInfo { get; }

        IFile AsIFile();
        string GetDownloadUrlWithName();
        Task<WebResult<string>> GetStreamUrlAsync(OneDriveWebController controller = null);
        Task<WebResult<OneDriveItemPage<ThumbnailSet>>> GetThumbnailsAsync(OneDriveWebController controller = null);
    }
}