using htmos.dtos;
using htmos.model;

namespace htmos.contract;

public interface IAppointmentRepository
{
    Task<BoolResult> BookAppointment(AppointmentDTO appointment);
    Task<IEnumerable<AppointmentResponseDTO>> ReadAllApointmentAsync(int ID);
}