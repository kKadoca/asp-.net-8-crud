namespace BarbershopApi.DTOs;

public record CreateAppointmentRequest(DateTime DateTime, int CustomerId, int ProfessionalId, int PrestationOfServiceId);
public record UpdateAppointmentRequest(DateTime DateTime, string Status, int CustomerId, int ProfessionalId, int PrestationOfServiceId);
public record AppointmentResponse(
    int Id,
    DateTime DateTime,
    string Status,
    CustomerResponse Customer,
    ProfessionalResponse Professional,
    PrestationOfServiceResponse PrestationOfService);
