using System.ComponentModel.DataAnnotations;

namespace game_store_backend.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Required] DateOnly ReleaseDate,
    [Range(0, 100)] decimal Price
);