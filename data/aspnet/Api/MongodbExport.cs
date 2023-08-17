using CockpitApp.Api.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

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

        public void Start(string outputDir)
        {
            var pageList = new DocumentList<PageItem>(_db.GetCollection<BsonDocument>("pages"));
            var revisionList = new DocumentList<RevisionItem>(_db.GetCollection<BsonDocument>("revisions"));

            Dictionary<string, RevisionItem> dedupulicated = new();
            revisionList.ForEach(x =>
            {
                x.SetPagePath(pageList);
                if (x.PagePath.StartsWith("/user/") ||
                    x.PagePath.StartsWith("/Contents") ||
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

            string tempOutput = "output";
            if (!Directory.Exists(tempOutput))
            {
                Directory.CreateDirectory(tempOutput);
            }
            dedupulicated.Values.ToList().ForEach(x => x.Output(tempOutput));
            
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
            string outputFile = Path.Combine(outputDir, "mongoExport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip");
            Zipper.Compress(tempOutput, outputFile);

            //  Upload minio (環境依存が強すぎる・・・)
            using(var proc = new Process()){
                proc.StartInfo.FileName = "mc";
                proc.StartInfo.Arguments = $"cp {outputFile} qedit/growi-backup";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
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
