using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;

public class CosmosService
{
    private readonly Container _container;

    public CosmosService(IConfiguration config)
    {
        var client = new CosmosClient(
            config["CosmosDb:ConnectionString"],
            new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

        _container = client.GetContainer(
            config["CosmosDb:DatabaseName"],
            config["CosmosDb:ContainerName"]);
    }

    public async Task AddAsync(SupportMessage message)
    {
        await _container.CreateItemAsync(message, new PartitionKey(message.Category));
    }

    public async Task<List<SupportMessage>> GetAllAsync()
    {
        var results = new List<SupportMessage>();
        var iterator = _container.GetItemQueryIterator<SupportMessage>("SELECT * FROM c");
        while (iterator.HasMoreResults)
        {
            var page = await iterator.ReadNextAsync();
            results.AddRange(page);
        }
        return results;
    }
}
