namespace BarbershopApi.Models;

public class Professional : ISoftDeletable
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = [];
}
