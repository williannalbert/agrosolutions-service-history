using System.Text.Json.Serialization;

namespace AgroSolutions.History.Application.DTOs.ReadingPayloads;

public record SiloDataDto(
    [property: JsonPropertyName("fill_level_percent")] double FillLevelPercent,
    [property: JsonPropertyName("avg_temp_celsius")] double InternalTempCelsius,
    [property: JsonPropertyName("co2_ppm")] double GasConcentrationPpm
);