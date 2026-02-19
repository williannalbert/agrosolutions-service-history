namespace AgroSolutions.History.Domain.ValueObjects.SensorData;
public record SiloData(
    double FillLevelPercent,
    double AvgTempCelsius,
    double Co2Ppm
) : SensorData;
