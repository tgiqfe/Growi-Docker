using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CockpitApp.Api.MongoDB
{
    internal class DocumentList<T> : List<T> where T : DocumentItem, new()
    {
        public DocumentList(IMongoCollection<BsonDocument> collection)
        {
            collection.Find(new BsonDocument()).
                ToList().
                ForEach(x =>
                {
                    var item = new T();
                    item.SetupProperty(x);
                    this.Add(item);
                });
        }
    }
}
