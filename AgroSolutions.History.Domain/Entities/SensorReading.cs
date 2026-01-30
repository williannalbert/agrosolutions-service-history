using AgroSolutions.History.Domain.Enums;
using AgroSolutions.History.Domain.ValueObjects;

namespace AgroSolutions.History.Domain.Entities;

public class SensorReading
{
    public Guid Id { get; private set; }
    public string FieldId { get; private set; }
    public string SensorId { get; private set; }
    public SensorType Type { get; private set; }
    public DateTime Timestamp { get; private set; }
    public SensorData Data { get; private set; }

    protected SensorReading() { }

    public SensorReading(string fieldId, string sensorId, SensorType type, DateTime timestamp, SensorData data)
    {
        if (string.IsNullOrWhiteSpace(fieldId))
            throw new ArgumentException("O ID do talhão (field_id) é obrigatório.");

        if (string.IsNullOrWhiteSpace(sensorId))
            throw new ArgumentException("O ID do sensor (sensor_id) é obrigatório.");

        if (data == null)
            throw new ArgumentException("Os dados do sensor não podem ser nulos.");

        ValidateDataTypeMatch(type, data);

        Id = Guid.NewGuid();
        FieldId = fieldId;
        SensorId = sensorId;
        Type = type;
        Timestamp = timestamp;
        Data = data;
    }

    private void ValidateDataTypeMatch(SensorType type, SensorData data)
    {
        bool isValid = type switch
        {
            SensorType.Solo => data is SoilData,
            SensorType.Silo => data is SiloData,
            SensorType.Meteorologica => data is WeatherData,
            _ => false
        };

        if (!isValid)
            throw new ArgumentException($"O tipo de dado '{data.GetType().Name}' não condiz com o tipo de sensor '{type}'.");
    }
}
