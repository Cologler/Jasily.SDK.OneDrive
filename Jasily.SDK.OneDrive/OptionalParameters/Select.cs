using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jasily.SDK.OneDrive.OptionalParameters
{
    public struct Select : IOneDriveOptionalParameters
    {
        public Select(IEnumerable<SelectProperties> selectProperties)
        {
            this.SelectProperties = selectProperties.ToArray();
        }

        public IReadOnlyCollection<SelectProperties> SelectProperties { get; }

        /// <summary>
        /// return value like 'select=name,size'
        /// </summary>
        /// <returns></returns>
        public string GetParameterString() => $"select={String.Join(",", this.Popup())}";

        private IEnumerable<string> Popup()
        {
            return this.SelectProperties.Select(z => z.ToString().ToLower());
        } 
    }
}