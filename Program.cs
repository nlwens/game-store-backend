using game_store_backend.Routers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();

app.MapGameRoutes();

app.Run();