namespace game_store_backend.Dtos;

public record GameSummaryDto(
    int Id,
    string Name,
    string Genre,
    DateOnly ReleaseDate,
    decimal Price
);