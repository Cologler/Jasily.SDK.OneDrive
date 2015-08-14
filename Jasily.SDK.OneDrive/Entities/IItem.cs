using System.Threading.Tasks;
using Jasily.Net;
using Jasily.SDK.OneDrive.Entities.Facets;

namespace Jasily.SDK.OneDrive.Entities
{
    public interface IItem : IEntity
    {
        string Id { get; }
        ItemReferenceFacet ParentInfo { get; }
        
        string GetPath();
        bool IsFile();
        IItem AsIItem();
        Task<WebResult<Item>> MoveByIdAsync(string targetFolderId, string newName = null, OneDriveWebController controller = null);
    }
}