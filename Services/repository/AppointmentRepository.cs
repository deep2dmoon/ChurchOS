using htmos.contract;
using htmos.data;
using htmos.dtos;
using htmos.model;
using Microsoft.EntityFrameworkCore;
namespace htmos.services.repository;

public class AppointmentRepository(DatabaseContext _context) : IAppointmentRepository
{
    public async Task<BoolResult> BookAppointment(AppointmentDTO appointment)
    {
        try
        {
            Appointment appointment_ = new() { Appointee = appointment.Appointee, BranchID = appointment.BranchID };
            _context.Appointments.Add(appointment_);
            await _context.SaveChangesAsync();
            return new BoolResult(true, $"Appointment successfully booked @ {DateTime.UtcNow}");
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<AppointmentResponseDTO>> ReadAllApointmentAsync(int branchID)
    {
        return [.. _context.Appointments.AsNoTracking().OrderBy(a => a.EntryTime).Where(a => a.BranchID == branchID).Select(a => new AppointmentResponseDTO(a.Appointee, a.BranchID, a.EntryTime))];
    }
}