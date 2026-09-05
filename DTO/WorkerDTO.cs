using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class WorkerDTO(
    [Required] int MemberID,
    [Required] int DepartmentID
);