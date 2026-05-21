namespace BarbershopApi.Models;

public class Professional
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Specialty { get; set; }
    public required string Phone { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = [];
}
