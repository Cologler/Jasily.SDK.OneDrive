using System;
using System.Globalization;

namespace Jasily.SDK.OneDrive.Entities
{
    public abstract class OneDriveObject
    {
        public DateTime GetISODateTime(string value)
        {
            switch (value.Length)
            {
                case 23:
                    return DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss.ff'Z'", CultureInfo.InvariantCulture);
                case 24:
                    return DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'", CultureInfo.InvariantCulture);
                default:
                    return DateTime.ParseExact(value, $"yyyy'-'MM'-'dd'T'HH':'mm':'ss.{'f'.Repeat(value.Length - 21)}'Z'", CultureInfo.InvariantCulture);
            }
        }
    }
}