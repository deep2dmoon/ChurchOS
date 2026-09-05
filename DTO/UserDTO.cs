using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class UserDTO(
    [Required][StringLength(20)] string Email,
    [Required][StringLength(30)] string Password
);