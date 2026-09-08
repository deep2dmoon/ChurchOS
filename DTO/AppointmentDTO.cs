using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class AppointmentDTO(
    [Required][StringLength(30)] string Appointee,
    [Required] int BranchID
);