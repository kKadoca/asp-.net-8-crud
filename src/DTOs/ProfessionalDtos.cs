namespace BarbershopApi.DTOs;

public record CreateProfessionalRequest(string Name, string Phone);
public record UpdateProfessionalRequest(string Name, string Phone);
public record ProfessionalResponse(int Id, string Name, string Phone);
