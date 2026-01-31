using AgroSolutions.History.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgroSolutions.History.Application.DTOs;

public record CreateReadingRequest(
[property: JsonPropertyName("field_id")] Guid FieldId,
    [property: JsonPropertyName("sensor_id")] Guid SensorId,
    [property: JsonPropertyName("type_sensor")] string TypeSensor,
    [property: JsonPropertyName("time_stamp")] DateTime Timestamp,
    [property: JsonPropertyName("data")] JsonElement Data
);

public record ReadingResponse(
    Guid Id,
    Guid FieldId,
    Guid SensorId,
    string SensorTypeDescription,
    DateTime Timestamp,
    object Data
);

public record SoilDataDto(
    [property: JsonPropertyName("soil_moisture_percent")] double SoilMoisturePercent,
    [property: JsonPropertyName("soil_ph")] double SoilPh,
    [property: JsonPropertyName("nutrients")] SoilNutrientsDto Nutrients
);

public record SoilNutrientsDto(
    [property: JsonPropertyName("nitrogen_mg_kg")] double NitrogenMgKg,
    [property: JsonPropertyName("phosphorus_mg_kg")] double PhosphorusMgKg,
    [property: JsonPropertyName("potassium_mg_kg")] double PotassiumMgKg
);

public record SiloDataDto(
    [property: JsonPropertyName("fill_level_percent")] double FillLevelPercent,
    [property: JsonPropertyName("avg_temp_celsius")] double InternalTempCelsius, 
    [property: JsonPropertyName("co2_ppm")] double GasConcentrationPpm
);

public record WeatherDataDto(
    [property: JsonPropertyName("temp_celsius")] double AmbientTempCelsius,
    [property: JsonPropertyName("humidity_percent")] double HumidityPercent,
    [property: JsonPropertyName("wind_speed_kmh")] double WindSpeedKmh,
    [property: JsonPropertyName("wind_direction")] string? WindDirection,
    [property: JsonPropertyName("rain_mm_last_hour")] double RainMmLastHour,
    [property: JsonPropertyName("dew_point")] double? DewPoint
);