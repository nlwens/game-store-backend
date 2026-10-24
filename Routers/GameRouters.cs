namespace game_store_backend.Routers;
using game_store_backend.Dtos;

public static class GameRouters
{
    private static readonly List<GameDto> games =
    [
        new GameDto(1, "Game 1", "Genre 1", new DateOnly(2021, 1, 1), 59.99m),
        new GameDto(2, "Game 2", "Genre 2", new DateOnly(2022, 2, 2), 99.99m),
        new GameDto(3, "Game 3", "Genre 3", new DateOnly(2023, 3, 3), 69.99m),
    ];

    const string getGameEndpointName = "GetGame";

    public static void MapGameRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("", () => games);

        group.MapGet("{id}", (int id) =>
        {
            GameDto? game = games.Find(g => g.Id == id);
            return game != null ? Results.Ok(game) : Results.NotFound();
        }).WithName(getGameEndpointName);

        // POST
        group.MapPost("", (CreateGameDto newGame) =>
        {
            if (string.IsNullOrWhiteSpace(newGame.Name))
            {
                return Results.BadRequest("Name is required");
            }

            int newId = games.Count == 0 ? 1 : games.Max(g => g.Id) + 1;

            GameDto game = new(newId, newGame.Name, newGame.Genre, newGame.ReleaseDate, newGame.Price);
            games.Add(game);
            return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("{id}", (int id, UpdateGameDto updateGame) =>
        {
            int index = games.FindIndex(g => g.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(id, updateGame.Name, updateGame.Genre, updateGame.ReleaseDate, updateGame.Price);
            return Results.NoContent();
        });

        group.MapDelete("{id}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);
            return Results.NoContent();
        });
    }
}