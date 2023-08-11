using CockpitApp;
using System.Diagnostics;
using System.Reflection;
using System.Text;

Environment.CurrentDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

var builder = WebApplication.CreateBuilder(args);

string _allowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _allowSpecificOrigins,
    build =>
    {
        build.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

var ap = new ArgsParam(args);

app.UseCors(_allowSpecificOrigins);
app.Urls.Add($"http://{ap.Address}:{ap.Port}");

//  Empty Get,Post
app.MapGet("/", () => "");
app.MapPost("/", () => "");

//  API Run Server side script.
app.MapGet("/api/script/{name}", async (context) =>
{
    var res = new ResponseItem();

    string scriptName = context.Request.RouteValues["name"] as string;

    if(!Directory.Exists(ap.LogDir)){
        Directory.CreateDirectory(ap.LogDir);
    }
    using (var sw = new StreamWriter(ap.LogPath, true, Encoding.UTF8))
    {
        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " " + scriptName);
    }
    if(Directory.Exists(ap.ScriptDir)){
    using (var proc = new Process())
    {
        proc.StartInfo.FileName = Path.Combine(ap.ScriptDir, scriptName);
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.UseShellExecute = false;
        proc.Start();
    }
    }


    res.Message = scriptName;
    await context.Response.WriteAsJsonAsync(res);
}).RequireCors(_allowSpecificOrigins);

app.Run();
