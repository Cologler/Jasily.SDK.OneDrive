using System.Enums;

namespace Jasily.SDK.OneDrive.OptionalParameters
{
    public enum ExpandMode
    {
        [SupportedFlags((int)(SelectProperties.Name | SelectProperties.Size))]
        Children,

        [SupportedFlags((int)(SelectProperties.Large | SelectProperties.Medium | SelectProperties.Small | SelectProperties.Source))]
        Thumbnails
    }
}