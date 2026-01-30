namespace AgroSolutions.History.Application.DTOs;

public record CreateReadingRequest(
    string FieldId,
    string SensorId,
    SensorType Type,
    DateTime Timestamp,
    object Data // O JSON dinâmico vem aqui
);

public record ReadingResponse(
    string Id, 
    string FieldId,
    string SensorId,
    SensorType Type,
    DateTime Timestamp,
    object Data
);