using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CockpitApp.Api.MongoDB
{
    internal class DocumentItem
    {
        public void SetupProperty(BsonDocument document)
        {
            var props = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var prop in props)
            {
                Type type = prop.PropertyType;

                DocumentPropertyAttribute attrib = prop.GetCustomAttribute<DocumentPropertyAttribute>();
                if (attrib.External) { continue; }

                string key = attrib?.Name ?? prop.Name;
                if (document.Contains(key))
                {
                    if (type == typeof(string))
                    {
                        prop.SetValue(this, document[key].ToString());
                    }
                    else if (type == typeof(int))
                    {
                        prop.SetValue(this, int.Parse(document[key].ToString()));
                    }
                    else if (type == typeof(DateTime))
                    {
                        prop.SetValue(this, DateTime.Parse(document[key].ToString()));
                    }
                }
            }
        }
    }
}
