﻿using CockpitApp.Api.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CockpitApp.Api
{
    internal class MongoDBExport
    {
        public int Code { get; private set; }

        public string DBServer { get; private set; }
        public int DBPort { get; private set; }
        public string DBName { get; private set; }

        private MongoClient _client = null;
        private IMongoDatabase _db = null;
        private GlobalParam _gp = null;

        public MongoDBExport(GlobalParam gp, string server, int port, string name)
        {
            _gp = gp;
            _client = new MongoClient($"mongodb://{server}:{port}");
            _db = _client.GetDatabase(name);
            this.DBServer = server;
            this.DBPort = port;
            this.DBName = name;
        }

        public void Start()
        {
            var pageList = new DocumentList<PageItem>(_db.GetCollection<BsonDocument>("pages"));
            var revisionList = new DocumentList<RevisionItem>(_db.GetCollection<BsonDocument>("revisions"));

            Dictionary<string, RevisionItem> dedupulicated = new();
            revisionList.ForEach(x =>
            {
                x.SetPagePath(pageList);
                if (x.PagePath == "/" ||
                    x.PagePath.StartsWith("/user/") ||
                    x.PagePath.StartsWith("Contents") ||
                    x.PagePath.StartsWith("/Sandbox"))
                {
                    //  Exclude page path
                }
                else
                {
                    if (dedupulicated.ContainsKey(x.PageId))
                    {
                        if (dedupulicated[x.PageId].CreatedAt <= x.CreatedAt)
                        {
                            dedupulicated[x.PageId] = x;
                        }
                    }
                    else
                    {
                        dedupulicated[x.PageId] = x;
                    }
                }
            });

            string targetDir = "output";
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }
            dedupulicated.Values.ToList().ForEach(x => x.Output(targetDir));

            Zipper.Compress(targetDir, "mongoExport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip");
        }

        public ResponseItem GetResult()
        {
            return new ResponseItem
            {
                Code = Code,
                Properties = new()
                {
                    { "DBServer", this.DBServer },
                    { "DBPort", this.DBPort.ToString() },
                    { "DBName", this.DBName },
                },
            };
        }
    }
}
