using AgroSolutions.History.Domain.Entities;
using AgroSolutions.History.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AgroSolutions.History.Infrastructure.Persistence.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDbSettings _settings;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<SensorReading> SensorReadings =>
        _database.GetCollection<SensorReading>(_settings.CollectionName);
}