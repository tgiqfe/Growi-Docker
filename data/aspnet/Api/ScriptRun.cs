using System.Diagnostics;

namespace CockpitApp.Api
{
    internal class ScriptRun
    {
        #region Exit code

        const int CODE_SUCCESS = 0;
        const int CODE_SCRIPT_IS_EMPTY = 1;
        const int CODE_SCRIPT_NOT_EXISTS = 2;
        const int CODE_SCRIPT_RUN_FAILED = 3;

        #endregion

        public int Code { get; private set; } = CODE_SUCCESS;

        public string ScriptName { get; private set; }
        public string ScriptPath { get; private set; }
        private GlobalParam _gp = null;

        public ScriptRun(GlobalParam gp, string scriptName)
        {
            _gp = gp;
            if (string.IsNullOrEmpty(scriptName))
            {
                this.Code = CODE_SCRIPT_IS_EMPTY;
                _gp.LogWrite("Script name is empty.");
            }
            else
            {
                this.ScriptName = scriptName;
                this.ScriptPath = Path.Combine(gp.ScriptDir, scriptName);
                _gp.LogWrite($"Script path: {ScriptPath}");
            }
        }

        public async void Start()
        {
            if (!File.Exists(ScriptPath))
            {
                this.Code = CODE_SCRIPT_NOT_EXISTS;
                _gp.LogWrite($"Script not exists [{ScriptPath}]");
                return;
            }

            await Task.Run(() =>
            {
                try{
                using (var proc = new Process())
                {
                    proc.StartInfo.FileName = ScriptPath;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    proc.WaitForExit();
                    _gp.LogWrite("Script run exit.");
                }
                }catch{
                    _gp.LogWrite("Script run failed.");
                    this.Code = CODE_SCRIPT_RUN_FAILED;
                }
            });
        }

        public ResponseItem GetResult()
        {
            return new ResponseItem()
            {
                Code = this.Code,
                Properties = new()
                {
                    { "ScriptName", this.ScriptName },
                    { "ScriptPath", this.ScriptPath },
                },
            };
        }
    }
}
