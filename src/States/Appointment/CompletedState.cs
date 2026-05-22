using BarbershopApi.Models;

namespace BarbershopApi.States.Appointment;

public class CompletedState : AppointmentState
{
    public override AppointmentStatus Status => AppointmentStatus.Completed;

    protected override bool CanTransitionTo(AppointmentStatus target) => false;
}
