using BarbershopApi.Exceptions;
using BarbershopApi.Models;

namespace BarbershopApi.States.Appointment;

public class ScheduledState : AppointmentState
{
    public override AppointmentStatus Status => AppointmentStatus.Scheduled;

    protected override bool CanTransitionTo(AppointmentStatus target) =>
        target is AppointmentStatus.Completed
               or AppointmentStatus.Cancelled
               or AppointmentStatus.NoShow;

    public override void ValidateDateTime(DateTime dateTime)
    {
        if (dateTime <= DateTime.UtcNow)
            throw new BusinessRuleException("A scheduled appointment must be set in the future.");
    }
}
