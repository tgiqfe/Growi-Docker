using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockpitApp.Api.MongoDB
{
    internal class DocumentPropertyAttribute : Attribute
    {
        public string Name { get; set; }

        public bool External { get; set; }

        public DocumentPropertyAttribute() { }

        public DocumentPropertyAttribute(string name)
        {
            this.Name = name;
        }
    }
}
