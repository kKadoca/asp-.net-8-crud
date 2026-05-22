namespace BarbershopApi.Models;

public class PrestationOfService : ISoftDeletable
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = [];
}
