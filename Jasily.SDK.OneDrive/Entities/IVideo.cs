namespace Jasily.SDK.OneDrive.Entities
{
    public interface IVideo : IFile
    {
        VideoInfo VideoInfo { get; set; }
    }
}