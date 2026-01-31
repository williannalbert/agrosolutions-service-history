namespace AgroSolutions.History.Application.DTOs.Responses;

public record ReadingResponse(
    Guid Id,
    Guid FieldId,
    Guid SensorId,
    string SensorTypeDescription,
    DateTime Timestamp,
    object Data
);