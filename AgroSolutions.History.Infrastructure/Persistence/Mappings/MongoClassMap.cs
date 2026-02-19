using AgroSolutions.History.Domain.ValueObjects.SensorData;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AgroSolutions.History.Infrastructure.Persistence.Mappings;

public static class MongoClassMap
{
    public static void RegisterClassMaps()
    {
        try
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
        catch (BsonSerializationException)
        {
        }

        if (BsonClassMap.IsClassMapRegistered(typeof(SensorData))) return;

        BsonClassMap.RegisterClassMap<SensorData>(cm =>
        {
            cm.AutoMap();
            cm.SetIsRootClass(true);
        });

        BsonClassMap.RegisterClassMap<SoilData>();
        BsonClassMap.RegisterClassMap<SiloData>();
        BsonClassMap.RegisterClassMap<WeatherData>();
    }
}