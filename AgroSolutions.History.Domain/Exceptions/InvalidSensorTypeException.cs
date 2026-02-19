namespace AgroSolutions.History.Domain.Exceptions;

public class InvalidSensorTypeException : DomainException
{
    public InvalidSensorTypeException(string type)
        : base($"O tipo de sensor '{type}' não é suportado ou é inválido.") { }
}