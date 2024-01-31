using GameDaOld.UI.Api;
using GameDaOld.UI.Api.Hubs;
using Microsoft.AspNetCore.Http.Connections;
var SignalROrigins = "_signalROrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SignalROrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                          policy.WithMethods("GET", "POST");
                          policy.AllowAnyHeader();
                          policy.AllowCredentials();
                      });
});

builder.Services.AddInjecoesDepedencias();
builder.AddCacheConfiguration();

var app = builder.Build();

app.UseRouting();
app.UseCors(SignalROrigins);
app.MapGet("/", () => "Hello World!");
app.MapHub<JogoDaVelhaHub>("/JogoDaVelhaHub", opt=>{
    opt.Transports = HttpTransportType.WebSockets;
});

app.Run();