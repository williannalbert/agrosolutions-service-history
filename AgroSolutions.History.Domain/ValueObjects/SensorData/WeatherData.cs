namespace AgroSolutions.History.Domain.ValueObjects.SensorData;
public record WeatherData(
    double TempCelsius,
    double HumidityPercent,
    double WindSpeedKmh,
    string WindDirection,
    double RainMmLastHour,
    double DewPoint
) : SensorData;