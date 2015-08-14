namespace Jasily.SDK.OneDrive.Entities
{
    public interface IImage : IFile
    {
        ImageInfo ImageInfo { get; }
        PhotoInfo PhotoInfo { get; }
    }
}