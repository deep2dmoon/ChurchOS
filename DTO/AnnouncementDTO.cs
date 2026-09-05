using System.ComponentModel.DataAnnotations;

namespace htmos.dtos;

public record class AnnouncementDTO(
    [Required][StringLength(50)] string Message
);