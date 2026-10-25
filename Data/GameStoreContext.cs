using Microsoft.EntityFrameworkCore;
using game_store_backend.Models;

namespace game_store_backend.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

}