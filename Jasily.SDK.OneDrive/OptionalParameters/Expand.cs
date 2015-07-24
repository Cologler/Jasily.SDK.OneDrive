using System.Diagnostics;
using System.Enums;
using System.Reflection;

namespace Jasily.SDK.OneDrive.OptionalParameters
{
    public struct Expand : IOneDriveOptionalParameters
    {
        public Expand(ExpandMode mode, Select? @select)
        {
            this.Mode = mode;
            this.Select = @select;
        }

        public Select? Select { get; }

        public ExpandMode Mode { get; }

        /// <summary>
        /// return value like 'expand=children(select=id,name)'
        /// </summary>
        /// <returns></returns>
        string IOneDriveOptionalParameters.GetParameterString()
        {
            if (!this.Select.HasValue)
                return $"expand={this.Mode.ToString().ToLower()}";

            var attr = Mode.GetType().GetRuntimeField(nameof(this.Mode)).GetCustomAttribute<SupportedFlagsAttribute>();
            Debug.Assert(attr != null);

            if (!attr.IsSupport((int)this.Select.Value.SelectedProperties))
                throw new System.NotSupportedException();

            return $"expand={this.Mode.ToString().ToLower()}({this.Select.Value.GetParameterString()})";
        }
    }
}