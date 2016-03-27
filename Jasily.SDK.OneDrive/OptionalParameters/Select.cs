using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jasily.SDK.OneDrive.OptionalParameters
{
    public struct Select : IOneDriveOptionalParameters
    {
        public Select(params SelectProperties[] selectProperties)
        {
            this.SelectedProperties = SelectProperties.None;
            foreach (var property in selectProperties)
                this.SelectedProperties |= property;
            if (this.SelectedProperties == SelectProperties.None)
                throw new ArgumentException("can not use 'SelectProperties.None'", nameof(selectProperties));
        }
        public Select(IEnumerable<SelectProperties> selectProperties)
        {
            this.SelectedProperties = SelectProperties.None;
            foreach (var property in selectProperties)
                this.SelectedProperties |= property;
            if (this.SelectedProperties == SelectProperties.None)
                throw new ArgumentException("can not use 'SelectProperties.None'", nameof(selectProperties));
        }

        public SelectProperties SelectedProperties { get; }

        /// <summary>
        /// return value like 'select=name,size'
        /// </summary>
        /// <returns></returns>
        public string GetParameterString() => $"select={String.Join(",", this.Popup())}";

        private IEnumerable<string> Popup()
        {
            var properties = this.SelectedProperties;
            return JasilyEnum.GetValues<SelectProperties>()
                .Where(z => z != SelectProperties.None && ((properties & z) == z))
                .Select(property => property.ToString().ToLower());
        }
    }
}