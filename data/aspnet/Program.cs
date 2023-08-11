using CockpitApp;

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
app.MapGet("/api/script{name}", async (context) =>
{
    string scriptName = context.Request.RouteValues["name"] as string;
    
});


app.Run();
