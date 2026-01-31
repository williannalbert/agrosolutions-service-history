using AgroSolutions.History.Application.DTOs;
using AgroSolutions.History.Application.Interfaces;
using AgroSolutions.History.Domain.Entities;
using AgroSolutions.History.Domain.Enums;
using AgroSolutions.History.Domain.Interfaces;
using AgroSolutions.History.Domain.ValueObjects;
using System.Text.Json;

namespace AgroSolutions.History.Application.Services;

public class SensorService : ISensorService
{
    private readonly ISensorRepository _repository;
    public SensorService(ISensorRepository repository)
    {
        _repository = repository;
    }
    public async Task<ReadingResponse> RegisterReadingAsync(CreateReadingRequest request)
    {
        if (!Enum.TryParse<SensorType>(request.TypeSensor, true, out var sensorTypeEnum))
        {
            throw new ArgumentException($"Tipo de sensor inválido: {request.TypeSensor}");
        }

        SensorData sensorDataDomain;

        switch (request.TypeSensor.ToLower())
        {
            case "solo":
                var soilDto = request.Data.Deserialize<SoilDataDto>();

                sensorDataDomain = new SoilData(
                    soilDto.SoilMoisturePercent,
                    soilDto.SoilPh,
                    new SoilNutrients(
                        soilDto.Nutrients.NitrogenMgKg,
                        soilDto.Nutrients.PhosphorusMgKg,
                        soilDto.Nutrients.PotassiumMgKg
                    )
                );
                break;

            case "silo":
                var siloDto = request.Data.Deserialize<SiloDataDto>();

                sensorDataDomain = new SiloData(
                    siloDto.FillLevelPercent,
                    siloDto.InternalTempCelsius, 
                    siloDto.GasConcentrationPpm
                );
                break;

            case "meteorologica":
                var weatherDto = request.Data.Deserialize<WeatherDataDto>();

                sensorDataDomain = new WeatherData(
                    weatherDto.AmbientTempCelsius, 
                    weatherDto.HumidityPercent,
                    weatherDto.WindSpeedKmh,
                    weatherDto.WindDirection ?? "N/A",
                    weatherDto.RainMmLastHour,
                    weatherDto.DewPoint ?? 0.0
                );
                break;

            default:
                throw new ArgumentException($"Tipo de sensor desconhecido: {request.TypeSensor}");
        }

        var reading = new SensorReading(
            request.FieldId,
            request.SensorId,
            sensorTypeEnum, 
            request.Timestamp,
            sensorDataDomain
        );

        await _repository.AddAsync(reading);

        return new ReadingResponse(
            reading.Id, 
            reading.FieldId,
            reading.SensorId,
            reading.Type.ToString(),
            reading.Timestamp,
            reading.Data
        );
    }
}
