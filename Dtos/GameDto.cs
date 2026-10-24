namespace game_store_backend.Dtos;

public record GameDto(
    int Id,
    string Name,
    string Genre,
    DateOnly ReleaseDate,
    decimal Price
);