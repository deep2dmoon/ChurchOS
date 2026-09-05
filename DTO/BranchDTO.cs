using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class BranchDTO(
    [Required][StringLength(20)] string Name,
    [Required][StringLength(30)] string Email,
    [Required][StringLength(30)] string Password
);