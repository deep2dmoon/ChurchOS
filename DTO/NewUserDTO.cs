using System.ComponentModel.DataAnnotations;

namespace htmos.model;

public record class NewUser(
    [Required][StringLength(20)] string Name,
    [Required][StringLength(20)] string Phone,
    int BranchID,
    [Required] int RoleID,
    [Required][StringLength(40)] string Email,
    string Password
);