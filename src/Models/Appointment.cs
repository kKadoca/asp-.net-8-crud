namespace BarbershopApi.Models;

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled,
    NoShow
}

public class Appointment : ISoftDeletable
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int ProfessionalId { get; set; }
    public Professional Professional { get; set; } = null!;

    public int PrestationOfServiceId { get; set; }
    public PrestationOfService PrestationOfService { get; set; } = null!;
}