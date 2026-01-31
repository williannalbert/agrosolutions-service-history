namespace AgroSolutions.History.Domain.ValueObjects.SensorData;

public record SoilData(
    double SoilMoisturePercent,
    double SoilPh,
    SoilNutrients Nutrients
) : SensorData;
