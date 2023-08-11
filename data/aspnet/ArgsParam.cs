namespace CockpitApp
{
    internal class ArgsParam
    {
        public string Address { get; set; } = "*";
        public int Port { get; set; } = 5000;

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
                }
            }
        }
    }
}