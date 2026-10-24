using System.ComponentModel.DataAnnotations;

namespace game_store_backend.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required] string Genre,
    [Required] DateOnly ReleaseDate,
    [Required][Range(0, 100)] decimal Price
);