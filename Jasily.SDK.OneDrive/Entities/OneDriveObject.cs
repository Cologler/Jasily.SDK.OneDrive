using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Jasily.SDK.OneDrive.Entities
{
    [DataContract]
    public abstract class OneDriveObject
    {
        public DateTime GetISODateTime(string value)
        {
            if (value.IsNullOrWhiteSpace())
                throw new FormatException($"{nameof(value)} IsNullOrWhiteSpace().");

            if (value.Length < 20)
                throw new FormatException($"{nameof(value)} length < 20.");


            switch (value.Length)
            {
                case 20:
                    return DateTime.ParseExact(value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture);
                case 21:
                    throw new FormatException($"{nameof(value)}.Length == 21.");
                default:
                    return DateTime.ParseExact(value, $"yyyy'-'MM'-'dd'T'HH':'mm':'ss.{'f'.Repeat(value.Length - 21)}'Z'", CultureInfo.InvariantCulture);
            }
        }
    }
}