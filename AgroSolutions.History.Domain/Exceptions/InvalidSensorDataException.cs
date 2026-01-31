namespace AgroSolutions.History.Domain.Exceptions;

public class InvalidSensorDataException : DomainException
{
    public InvalidSensorDataException(string details)
        : base($"Os dados do sensor são inválidos: {details}") { }
}