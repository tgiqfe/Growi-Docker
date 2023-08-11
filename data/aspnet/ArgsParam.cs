namespace CockpitApp
{
    internal class ArgsParam
    {
        #region publci parameters

        public string Address { get; private set; } = "*";

        public int Port { get; private set; } = 5000;

        public string LogDir { get; private set; } = "logs";

        public string LogPath
        {
            get
            {
                return Path.Combine(this.LogDir, "cockpitapp_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
            }
        }

        public string ScriptDir { get; private set; } = "scripts";

        #endregion

        public ArgsParam(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "/a":
                    case "-a":
                    case "/address":
                    case "--address":
                        this.Address = args[++i];
                        break;
                    case "/p":
                    case "-p":
                    case "/port":
                    case "--port":
                        if (int.TryParse(args[++i], out int num))
                        {
                            this.Port = num;
                        }
                        break;
                    case "/l":
                    case "-l":
                    case "/logs":
                    case "--logs":
                        this.LogDir = args[++i];
                        break;
                    case "/s":
                    case "-s":
                    case "/scriptdir":
                    case "--scriptdir":
                        this.ScriptDir = args[++i];
                        break;
                }
            }
        }
    }
}