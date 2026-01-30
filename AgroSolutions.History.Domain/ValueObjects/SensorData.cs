namespace AgroSolutions.History.Domain.ValueObjects;

public abstract record SensorData;

public record SoilData(
    double SoilMoisturePercent,
    double SoilPh,
    SoilNutrients Nutrients
) : SensorData;

public record SoilNutrients(
    double NitrogenMgKg,
    double PhosphorusMgKg,
    double PotassiumMgKg
);

public record SiloData(
    string SiloId,
    double FillLevelPercent,
    double AvgTempCelsius,
    double Co2Ppm
) : SensorData;

public record WeatherData(
    double TempCelsius,
    double HumidityPercent,
    double WindSpeedKmh,
    string WindDirection,
    double RainMmLastHour,
    double DewPoint
) : SensorData;