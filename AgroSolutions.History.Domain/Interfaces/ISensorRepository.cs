using AgroSolutions.History.Domain.Entities;

namespace AgroSolutions.History.Domain.Interfaces;

public interface ISensorRepository
{
    Task AddAsync(SensorReading reading);
    // Task<IEnumerable<SensorReading>> GetByFieldAsync(string fieldId);
}