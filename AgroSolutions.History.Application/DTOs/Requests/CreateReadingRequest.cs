using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgroSolutions.History.Application.DTOs.Requests;

public record CreateReadingRequest(
[property: JsonPropertyName("field_id")] Guid FieldId,
    [property: JsonPropertyName("sensor_id")] Guid SensorId,
    [property: JsonPropertyName("type_sensor")] string TypeSensor,
    [property: JsonPropertyName("time_stamp")] DateTime Timestamp,
    [property: JsonPropertyName("data")] JsonElement Data
);