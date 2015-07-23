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
            return this.Select.HasValue
                ? $"expand={this.Mode.ToString().ToLower()}({this.Select.Value.GetParameterString()})"
                : $"expand={this.Mode.ToString().ToLower()}";
        }
    }
}