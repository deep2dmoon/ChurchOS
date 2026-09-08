using System.Security.Claims;
using htmos.branch;
using htmos.dtos;
using htmos.model;
using htmos.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace htmos.controller;

[ApiController]
[Route("api/appointments")]
public class AppointementController(AppointmentService appointmentService, IBranchRepository branchRepository) : ControllerBase
{
    [HttpPost("booking")]
    public async Task<IActionResult> BookAppointment(AppointmentDTO appointmentDTO)
    {
        BoolResult result = await appointmentService.Book(appointmentDTO);
        return Ok(result);
    }

    [HttpGet("all")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> GetAppointmentsAsync()
    {
        int AdminID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        Branch branch = await branchRepository.GetBranchAsync(AdminID);

        IEnumerable<AppointmentResponseDTO> appointments = await appointmentService.ReadAllAppointmentAsync(branch.ID);
        return Ok(appointments);
    }
}