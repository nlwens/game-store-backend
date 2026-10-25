namespace game_store_backend.Dtos;

public record GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    DateOnly ReleaseDate,
    decimal Price
);