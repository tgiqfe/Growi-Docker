using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockpitApp.Api.MongoDB
{
    internal class PageItem : DocumentItem
    {
        [DocumentProperty("_id")]
        public string Id { get; set; }

        [DocumentProperty("revision")]
        public string RevisionId { get; set; }

        [DocumentProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// for Debug
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Id} {RevisionId} {Path}";
        }
    }
}
