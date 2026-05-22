using BarbershopApi.Models;

namespace BarbershopApi.States.Appointment;

public class NoShowState : AppointmentState
{
    public override AppointmentStatus Status => AppointmentStatus.NoShow;

    protected override bool CanTransitionTo(AppointmentStatus target) => false;
}
