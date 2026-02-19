using AgroSolutions.History.Domain.Enums;

namespace AgroSolutions.History.Domain.ValueObjects.Filters;

public record ReadingFilter(
    List<Guid>? FieldIds,
    List<Guid>? SensorIds,
    List<SensorType>? Types,
    DateTime? StartDate,
    DateTime? EndDate,
    bool Ascending
);