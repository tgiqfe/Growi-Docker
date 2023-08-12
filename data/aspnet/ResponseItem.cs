namespace CockpitApp
{
    internal class ResponseItem
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}
