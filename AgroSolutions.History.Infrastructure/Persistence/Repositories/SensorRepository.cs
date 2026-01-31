using AgroSolutions.History.Domain.Entities;
using AgroSolutions.History.Domain.Interfaces;
using AgroSolutions.History.Infrastructure.Persistence.Context;

namespace AgroSolutions.History.Infrastructure.Persistence.Repositories;

public class SensorRepository : ISensorRepository
{
    private readonly MongoDbContext _context;

    public SensorRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SensorReading reading)
    {
        await _context.SensorReadings.InsertOneAsync(reading);
    }
}