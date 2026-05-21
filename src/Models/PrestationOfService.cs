namespace BarbershopApi.Models;

public class PrestationOfService
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = [];
}
