using System.Threading.Tasks;
using Jasily.Net;
using Jasily.SDK.OneDrive.Entities.Facets;

namespace Jasily.SDK.OneDrive.Entities
{
    public interface IItem
    {
        ItemReferenceFacet ParentInfo { get; }

        string GetPath();
        bool IsFile();
        Task<WebResult<Item>> MoveByIdAsync(string targetFolderId, string newName = null, OneDriveWebController controller = null);
    }
}