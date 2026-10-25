using game_store_backend.Routers;
using game_store_backend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();

var app = builder.Build();

app.MapGameRoutes();
app.MapGenreRoutes();

app.MigrateDb();

app.Run();