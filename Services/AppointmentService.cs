using htmos.contract;
using htmos.dtos;

namespace htmos.services;

public class AppointmentService(IAppointmentRepository appointmentRepository)
{
    public async Task<BoolResult> Book(AppointmentDTO appointmentDTO)
    {
        BoolResult result = await appointmentRepository.BookAppointment(appointmentDTO);
        return result;
    }

    public async Task<IEnumerable<AppointmentResponseDTO>> ReadAllAppointmentAsync(int ID)
    {
        return await appointmentRepository.ReadAllApointmentAsync(ID);
    }
}