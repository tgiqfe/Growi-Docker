namespace CockpitApp.Api
{
    internal class MongodbExport
    {
        public int Code { get; private set; }

        public MongodbExport()
        {

        }

        public ResponseItem GetResult()
        {
            return new ResponseItem
            {
                Code = this.Code,
            };
        }
    }
}
