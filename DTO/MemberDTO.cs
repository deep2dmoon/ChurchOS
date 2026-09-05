using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class MemberDTO(
    [Required][StringLength(40)] string Name,
    [Required][StringLength(30)] string Phone,
    [Required] bool IsFirstTimer
);