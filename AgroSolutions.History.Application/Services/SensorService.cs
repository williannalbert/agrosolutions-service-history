using AgroSolutions.History.Application.DTOs.ReadingPayloads;
using AgroSolutions.History.Application.DTOs.Requests;
using AgroSolutions.History.Application.DTOs.Responses;
using AgroSolutions.History.Application.Interfaces;
using AgroSolutions.History.Application.Mappers;
using AgroSolutions.History.Domain.Entities;
using AgroSolutions.History.Domain.Enums;
using AgroSolutions.History.Domain.Exceptions;
using AgroSolutions.History.Domain.Interfaces;
using AgroSolutions.History.Domain.ValueObjects.Filters;
using AgroSolutions.History.Domain.ValueObjects.SensorData;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AgroSolutions.History.Application.Services;

public class SensorService : ISensorService
{
    private readonly ISensorRepository _repository;
    private readonly ILogger<SensorService> _logger;
    public SensorService(ISensorRepository repository, ILogger<SensorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<ReadingResponse> RegisterReadingAsync(CreateReadingRequest request)
    {
        _logger.LogInformation("Iniciando registro de sensor. Sensor: {SensorId}, Tipo: {Type}",
            request.SensorId, request.TypeSensor);

        if (!Enum.TryParse<SensorType>(request.TypeSensor, true, out var sensorTypeEnum))
        {
            throw new InvalidSensorTypeException($"Tipo de sensor inválido: {request.TypeSensor}");
        }

        SensorData sensorDataDomain = SensorDataMapper.ToDomain(sensorTypeEnum, request.Data);

        var reading = new SensorReading(
            request.FieldId,
            request.SensorId,
            sensorTypeEnum, 
            request.Timestamp,
            sensorDataDomain
        );

        await _repository.AddAsync(reading);

        _logger.LogInformation("Sensor registrado com sucesso. ID Gerado: {Id}", reading.Id);

        return new ReadingResponse(
            reading.Id, 
            reading.FieldId,
            reading.SensorId,
            reading.Type.ToString(),
            reading.Timestamp,
            reading.Data
        );
    }

    public async Task<IEnumerable<ReadingResponse>> GetReadingsAsync(GetReadingsQuery query)
    {
        List<SensorType>? typesEnum = null;
        if (query.TypeSensors != null && query.TypeSensors.Length > 0)
        {
            typesEnum = new List<SensorType>();

            foreach (var typeString in query.TypeSensors)
            {
                if (Enum.TryParse<SensorType>(typeString, true, out var parsed))
                {
                    typesEnum.Add(parsed);
                }
            }
        }

        var fieldIdsList = query.FieldIds?.ToList();
        var sensorIdsList = query.SensorIds?.ToList();

        var filter = new ReadingFilter(
            fieldIdsList,
            sensorIdsList,
            typesEnum,
            query.StartDate,
            query.EndDate,
            query.Ascending
        );

        var readings = await _repository.GetAllAsync(filter);

        return readings.Select(r => new ReadingResponse(
            r.Id,
            r.FieldId,
            r.SensorId,
            r.Type.ToString(),
            r.Timestamp,
            r.Data 
        ));
    }
}
