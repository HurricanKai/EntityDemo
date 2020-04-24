using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EntityDemo
{
    public class Test : IHostedService
    {
        private readonly IRequestClient<CreateEntityRequest> _createClient;
        private readonly IRequestClient<GetEntityMetadataRequest> _getMetadataClient;
        private readonly IEntityMetadataSerializer _serializer;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly ILogger _logger;

        public Test(IRequestClient<CreateEntityRequest> createClient, IRequestClient<GetEntityMetadataRequest> getMetadataClient, IEntityMetadataSerializer serializer, ISendEndpointProvider sendEndpointProvider, ILogger<Test> logger)
        {
            _createClient = createClient;
            _getMetadataClient = getMetadataClient;
            _serializer = serializer;
            _sendEndpointProvider = sendEndpointProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(Run);
            return Task.CompletedTask;
        }

        private async Task Run()
        {
            var id = (await _createClient.GetResponse<CreateEntityResponse>(new {TypeId = 1})).Message.EntityId;
            _logger.LogInformation($"Test ID: {id:D}");
            var s =Stopwatch.StartNew();
            while (true)
            {
                var first = (await _getMetadataClient.GetResponse<GetEntityMetadataResponse>(new
                    {EntityId = id, Index = 0, Type = 0})).Message;
                var old = (int)_serializer.Deserialize(first.Type, first.Data);
                _logger.LogInformation($"Old Value: {old}");

                if (old >= 10000)
                    break;
                
                await _sendEndpointProvider.Send<UpdateEntityMetadata>(new
                    {EntityId = id, Index = 0, Type = 0, Data = _serializer.Serialize(0, old + 1)});
                var second =
                    (await _getMetadataClient.GetResponse<GetEntityMetadataResponse>(new
                        {EntityId = id, Index = 0, Type = 0})).Message;
                var @new = (int)_serializer.Deserialize(second.Type, second.Data);
                _logger.LogInformation($"New Value: {@new}");
            }
            s.Stop();
            _logger.LogInformation($"Took {s.Elapsed}");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}