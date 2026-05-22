using BarbershopApi.Models;

namespace BarbershopApi.States.Appointment;

public class CancelledState : AppointmentState
{
    public override AppointmentStatus Status => AppointmentStatus.Cancelled;

    protected override bool CanTransitionTo(AppointmentStatus target) => false;
}
