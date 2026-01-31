using System.Text.Json.Serialization;

namespace AgroSolutions.History.Application.DTOs.ReadingPayloads;

public record WeatherDataDto(
    [property: JsonPropertyName("temp_celsius")] double AmbientTempCelsius,
    [property: JsonPropertyName("humidity_percent")] double HumidityPercent,
    [property: JsonPropertyName("wind_speed_kmh")] double WindSpeedKmh,
    [property: JsonPropertyName("wind_direction")] string? WindDirection,
    [property: JsonPropertyName("rain_mm_last_hour")] double RainMmLastHour,
    [property: JsonPropertyName("dew_point")] double? DewPoint
);