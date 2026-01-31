using AgroSolutions.History.Application.DTOs.ReadingPayloads;
using AgroSolutions.History.Domain.Enums;
using AgroSolutions.History.Domain.Exceptions;
using AgroSolutions.History.Domain.ValueObjects.SensorData;
using System.Text.Json;

namespace AgroSolutions.History.Application.Mappers;

public static class SensorDataMapper
{
    public static SensorData ToDomain(SensorType type, JsonElement data)
    {
        return type switch
        {
            SensorType.Solo => MapSoil(data),
            SensorType.Silo => MapSilo(data),
            SensorType.Meteorologica => MapWeather(data),
            _ => throw new NotImplementedException($"Mapper não implementado para: {type}")
        };
    }

    private static SoilData MapSoil(JsonElement data)
    {
        var dto = data.Deserialize<SoilDataDto>();
        // Aqui já usamos uma Exception personalizada (veremos abaixo)
        if (dto == null) throw new InvalidSensorDataException("Dados do solo inválidos ou mal formatados.");

        return new SoilData(
            dto.SoilMoisturePercent,
            dto.SoilPh,
            new SoilNutrients(
                dto.Nutrients.NitrogenMgKg,
                dto.Nutrients.PhosphorusMgKg,
                dto.Nutrients.PotassiumMgKg
            )
        );
    }

    private static SiloData MapSilo(JsonElement data)
    {
        var dto = data.Deserialize<SiloDataDto>();
        if (dto == null) throw new InvalidSensorDataException("Dados do silo inválidos.");

        return new SiloData(
            dto.FillLevelPercent,
            dto.InternalTempCelsius,
            dto.GasConcentrationPpm
        );
    }

    private static WeatherData MapWeather(JsonElement data)
    {
        var dto = data.Deserialize<WeatherDataDto>();
        if (dto == null) throw new InvalidSensorDataException("Dados meteorológicos inválidos.");

        return new WeatherData(
            dto.AmbientTempCelsius,
            dto.HumidityPercent,
            dto.WindSpeedKmh,
            dto.WindDirection ?? "N/A",
            dto.RainMmLastHour,
            dto.DewPoint ?? 0.0
        );
    }
}
