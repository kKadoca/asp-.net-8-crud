using BarbershopApi.Exceptions;
using BarbershopApi.Models;

namespace BarbershopApi.States.Appointment;

public abstract class AppointmentState
{
    public abstract AppointmentStatus Status { get; }

    protected abstract bool CanTransitionTo(AppointmentStatus target);

    public virtual void ValidateDateTime(DateTime dateTime) { }

    public void EnsureCanTransitionTo(AppointmentStatus target)
    {
        if (!CanTransitionTo(target))
            throw new InvalidStateTransitionException(Status.ToString(), target.ToString());
    }

    public static AppointmentState For(AppointmentStatus status) => status switch
    {
        AppointmentStatus.Scheduled => new ScheduledState(),
        AppointmentStatus.Completed => new CompletedState(),
        AppointmentStatus.Cancelled => new CancelledState(),
        AppointmentStatus.NoShow    => new NoShowState(),
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };
}
