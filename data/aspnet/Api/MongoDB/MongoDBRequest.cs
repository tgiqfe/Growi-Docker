namespace CockpitApp.Api.MongoDB
{
    public class MongoDBRequest
    {
        public string dbServer { get; set; }
        public int dbPort { get; set; }
        public string dbName { get; set; }
        public string outputDir { get; set; }
    }
}
