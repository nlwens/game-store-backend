namespace game_store_backend.Routers;

using game_store_backend.Data;
using game_store_backend.Dtos;
using game_store_backend.Models;
using Microsoft.EntityFrameworkCore;

public static class GameRouters
{
    const string getGameEndpointName = "GetGame";

    public static void MapGameRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET all games
        group.MapGet("", async (GameStoreContext dbContext) => 
            await dbContext.Games
                           .Include(g => g.Genre)
                           .Select(g => new GameSummaryDto(
                                g.Id, 
                                g.Name, 
                                g.Genre!.Name, 
                                g.ReleaseDate, 
                                g.Price
                            ))
                            .AsNoTracking()
                            .ToListAsync());

        // GET a game by id
        group.MapGet("{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);
            return game != null ? Results.Ok(new GameDetailsDto(
                game.Id, 
                game.Name, 
                game.GenreId, 
                game.ReleaseDate, 
                game.Price
            )) : Results.NotFound();
        }).WithName(getGameEndpointName);

        // POST a new game
        group.MapPost("", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                ReleaseDate = newGame.ReleaseDate,
                Price = newGame.Price,
            };
            
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDetails = new(
                game.Id, 
                game.Name, 
                game.GenreId, 
                game.ReleaseDate, 
                game.Price
            );

            return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, gameDetails);
        });

        // PUT a game by id
        group.MapPut("{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            if (game == null)
            {
                return Results.NotFound();
            }

            game.Name = updateGame.Name;
            game.GenreId = updateGame.GenreId;
            game.ReleaseDate = updateGame.ReleaseDate;
            game.Price = updateGame.Price;

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        // DELETE a game by id
        group.MapDelete("{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                            .Where(g => g.Id == id)
                            .ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }
}