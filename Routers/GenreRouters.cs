using game_store_backend.Data;
using game_store_backend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace game_store_backend.Routers;

public static class GenreRouters
{
    public static void MapGenreRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET /genres
        group.MapGet("", async (GameStoreContext dbContext) =>
        await dbContext.Genres
                        .Select(genre => new GenreDto(genre.Id, genre.Name))
                        .AsNoTracking()
                        .ToListAsync());
    }
}