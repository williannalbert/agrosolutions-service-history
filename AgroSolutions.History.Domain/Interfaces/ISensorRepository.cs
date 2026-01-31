using AgroSolutions.History.Domain.Entities;
using AgroSolutions.History.Domain.ValueObjects.Filters;

namespace AgroSolutions.History.Domain.Interfaces;

public interface ISensorRepository
{
    Task AddAsync(SensorReading reading);
    Task<IEnumerable<SensorReading>> GetAllAsync(ReadingFilter filter);
}