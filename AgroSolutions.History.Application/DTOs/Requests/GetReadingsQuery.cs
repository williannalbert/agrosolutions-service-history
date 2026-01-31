using Microsoft.AspNetCore.Mvc;

namespace AgroSolutions.History.Application.DTOs.Requests;

public record GetReadingsQuery
{
    [FromQuery(Name = "field_id")]
    public Guid[]? FieldIds { get; init; }

    [FromQuery(Name = "sensor_id")]
    public Guid[]? SensorIds { get; init; }

    [FromQuery(Name = "type_sensor")]
    public string[]? TypeSensors { get; init; }

    [FromQuery(Name = "start_date")]
    public DateTime? StartDate { get; init; }

    [FromQuery(Name = "end_date")]
    public DateTime? EndDate { get; init; }

    [FromQuery(Name = "ascending")]
    public bool Ascending { get; init; } = false;
}
