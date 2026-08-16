using Microsoft.Azure.Cosmos;
using Azure.Identity;
using Azure.Core;
using Microsoft.Azure.Cosmos.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/getdata", async () =>
{
    TokenCredential credential = new DefaultAzureCredential();

    CosmosClient client = new("https://cosmosdbprivateendpoint.documents.azure.com:443/", credential);

    Database database = client.GetDatabase("cosmosDBPrivateEndpointDatabase");

    Container container = database.GetContainer("cosmosDBPrivateEndpointContainer");

    FeedIterator<dynamic> orders = container.GetItemLinqQueryable<dynamic>().ToFeedIterator();

    while (orders.HasMoreResults)
    {
        FeedResponse<dynamic> response = await orders.ReadNextAsync();
        foreach (dynamic item in response)
        {
            // just return the first item found
            return item;
        }
    }

    // the default case
    return null;
})
.WithName("GetData")
.WithOpenApi();

app.Run();
