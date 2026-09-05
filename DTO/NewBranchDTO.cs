using System.ComponentModel.DataAnnotations;

namespace htmos.model;

public record class NewBranchDto(
    [Required][StringLength(20)] string Name,
    [Required][StringLength(20)] string Phone,
    [Required] int RoleID,
    [Required][StringLength(40)] string Email,
    string Password,
    string Branch
);