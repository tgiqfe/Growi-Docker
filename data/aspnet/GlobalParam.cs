using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CockpitApp
{
    internal class GlobalParam
    {
        #region publci parameters

        /// <summary>
        /// Valid Address
        /// </summary>
        public string Address { get; private set; } = "*";

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; private set; } = 5000;

        /// <summary>
        /// Log output parameter
        /// </summary>
        public string LogDir { get; private set; } = "logs";
        public string LogPath
        {
            get
            {
                return Path.Combine(this.LogDir, "cockpitapp_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
            }
        }

        /// <summary>
        /// Stored scripts directory
        /// </summary>
        public string ScriptDir { get; private set; } = "scripts";

        #endregion

        public GlobalParam(string[] args)
        {
            //  Set argumnets
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

            //  Set directory
            if (!Directory.Exists(this.LogDir))
            {
                Directory.CreateDirectory(this.LogDir);
            }
            if (!Directory.Exists(this.ScriptDir))
            {
                Directory.CreateDirectory(this.ScriptDir);
            }
        }

        private object lockObj = new();

        public void LogWrite(string message, LogType logType = LogType.Info)
        {
            lock (lockObj)
            {
                using (var sw = new StreamWriter(this.LogPath, true, Encoding.UTF8))
                {
                    string dt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    sw.WriteLine($"[{dt}][{logType}] {message}");
                }
            }
        }
    }
}