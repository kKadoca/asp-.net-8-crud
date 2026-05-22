namespace BarbershopApi.DTOs;

public record CreateCustomerRequest(string Name, string Email, string Phone);
public record UpdateCustomerRequest(string Name, string Email, string Phone);
public record CustomerResponse(int Id, string Name, string Email, string Phone);
