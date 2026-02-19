using System.Text.Json.Serialization;

namespace AgroSolutions.History.Application.DTOs.ReadingPayloads;

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