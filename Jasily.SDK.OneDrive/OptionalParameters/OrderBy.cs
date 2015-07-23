using System;

namespace Jasily.SDK.OneDrive.OptionalParameters
{
    public struct OrderBy : IOneDriveOptionalParameters
    {
        public OrderBy(OrderByProperties property, bool isDesc)
        {
            this.Property = property;
            this.IsDesc = isDesc;
        }

        public OrderByProperties Property { get; }

        public bool IsDesc { get; }

        public string GetParameterString() => $"orderby={GetValue(this.Property)} {GetValueByIsDesc(this.IsDesc)}";

        private static string GetValueByIsDesc(bool isDesc) => isDesc ? "desc" : "asc"; 

        private static string GetValue(OrderByProperties property)
        {
            switch (property)
            {
                case OrderByProperties.Name:
                    return "name";
                case OrderByProperties.Size:
                    return "size";
                case OrderByProperties.LastModifiedDateTime:
                    return "lastModifiedDateTime";
                default:
                    throw new ArgumentOutOfRangeException(nameof(property), property, null);
            }
        }
    }
}