namespace BarbershopApi.Exceptions;

public class InvalidStateTransitionException : BusinessRuleException
{
    public InvalidStateTransitionException(string from, string to)
        : base($"Cannot transition appointment from '{from}' to '{to}'.") { }
}
