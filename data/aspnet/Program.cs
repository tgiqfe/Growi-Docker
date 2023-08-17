using CockpitApp;
using CockpitApp.Api;
using System.Diagnostics;

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

//  Set global parameter
var gp = new GlobalParam(args);

app.UseCors(_allowSpecificOrigins);
app.Urls.Add($"http://{gp.Address}:{gp.Port}");

//  Empty Get,Post
app.MapGet("/", () => "");
app.MapPost("/", () => "");

//  API Run Server side script.
app.MapGet("/api/script/{name}", async (context) =>
{
    var scriptRun = new ScriptRun(
        gp,
        context.Request.RouteValues["name"] as string);
    scriptRun.Start();
    var res = scriptRun.GetResult();
    res.Message = "Script run.";

    await context.Response.WriteAsJsonAsync(res);
}).RequireCors(_allowSpecificOrigins);

//  API Export MongoDB (Get)
app.MapGet("/api/mongodb/export", async (context) =>
{
    string dbServer = context.Request.Query["server"].ToString();
    int dbPort = int.TryParse(context.Request.Query["port"].ToString(), out int num) ? num : 27017;
    string dbName = context.Request.Query["name"].ToString();

    var mongodbExport = new MongoDBExport(gp, dbServer, dbPort, dbName);
    mongodbExport.Start();
    var res = mongodbExport.GetResult();
    
    await context.Response.WriteAsJsonAsync(res);
}).RequireCors(_allowSpecificOrigins);

//  API Export MongoDB (Post)
app.MapPost("/api/mongodb/export", async (context) =>{
    
});


app.Run();
