using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class AppointmentResponseDTO(
     string Appointee,
      int BranchID,
      DateTime EntryTime

);