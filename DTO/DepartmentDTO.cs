using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class DepartmentDTO(
    [Required] string Name
);