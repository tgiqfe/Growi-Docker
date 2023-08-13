using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockpitApp.Api.MongoDB
{
    internal class RevisionItem : DocumentItem
    {
        [DocumentProperty("_id")]
        public string Id { get; set; }

        [DocumentProperty("pageId")]
        public string PageId { get; set; }

        [DocumentProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [DocumentProperty("body")]
        public string Body { get; set; }

        [DocumentProperty(External = true)]
        public string PagePath { get; set; }

        public void SetPagePath(DocumentList<PageItem> pageList)
        {
            var page = pageList.FirstOrDefault(x => x.Id == this.PageId);
            if (page != null)
            {
                this.PagePath = page.Path;
            }
        }

        public string GetPagePath()
        {
            return this.PagePath?.TrimStart('/').
                Replace("/", "_").
                Replace("\\", "_").
                Replace("\"", "”") ?? "";
        }

        public void Output(string TargetDir)
        {
            File.WriteAllText(Path.Combine(TargetDir, GetPagePath() + ".md"), this.Body);
        }

        /// <summary>
        /// for Debug
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string text = this.Body.Length < 15 ? this.Body : this.Body[0..15];
            text = text.Replace("\n", "\\n");

            return $"{Id} {GetPagePath()} {CreatedAt} {text}";
        }
    }
}
