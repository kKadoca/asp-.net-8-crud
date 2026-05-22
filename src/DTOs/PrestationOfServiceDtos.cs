namespace BarbershopApi.DTOs;

public record CreatePrestationOfServiceRequest(string Name, string Description, decimal Price, int DurationMinutes);
public record UpdatePrestationOfServiceRequest(string Name, string Description, decimal Price, int DurationMinutes);
public record PrestationOfServiceResponse(int Id, string Name, string Description, decimal Price, int DurationMinutes);
