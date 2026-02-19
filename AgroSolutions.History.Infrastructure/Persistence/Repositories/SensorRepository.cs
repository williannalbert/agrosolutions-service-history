using AgroSolutions.History.Domain.Entities;
using AgroSolutions.History.Domain.Interfaces;
using AgroSolutions.History.Domain.ValueObjects.Filters;
using AgroSolutions.History.Infrastructure.Persistence.Context;
using MongoDB.Driver;

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

    public async Task<IEnumerable<SensorReading>> GetAllAsync(ReadingFilter filter)
    {

        var query = ApplyFilters(filter);

        var findOptions = _context.SensorReadings.Find(query);

        findOptions = filter.Ascending ? findOptions.SortBy(x => x.Timestamp) : findOptions.SortByDescending(x => x.Timestamp);

        return await findOptions.ToListAsync();
    }

    private FilterDefinition<SensorReading> ApplyFilters(ReadingFilter filter)
    {
        var builder = Builders<SensorReading>.Filter;
        var query = builder.Empty;

        if (filter.FieldIds != null && filter.FieldIds.Any())
        {
            query &= builder.In(x => x.FieldId, filter.FieldIds);
        }

        if (filter.SensorIds != null && filter.SensorIds.Any())
        {
            query &= builder.In(x => x.SensorId, filter.SensorIds);
        }

        if (filter.Types != null && filter.Types.Any())
        {
            query &= builder.In(x => x.Type, filter.Types);
        }

        if (filter.StartDate.HasValue)
        {
            query &= builder.Gte(x => x.Timestamp, filter.StartDate.Value);
        }

        if (filter.EndDate.HasValue)
        {
            query &= builder.Lte(x => x.Timestamp, filter.EndDate.Value);
        }
        return query;
    }
}