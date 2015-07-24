using System;

namespace Jasily.SDK.OneDrive.OptionalParameters
{
    [Flags]
    public enum SelectProperties
    {
        /// <summary>
        /// do not use this property.
        /// </summary>
        None = 0,

        Name = 1,

        Size = 2,

        Large = 4,

        Medium = 8,

        Small = 16,

        Source = 32
    }
}