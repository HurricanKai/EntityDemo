using System.Threading.Tasks;
using MassTransit;

namespace EntityDemo
{
    public sealed class GetEntityMetadataConsumer
        : IConsumer<GetEntityMetadataRequest>
    {
        private readonly IEntityManager _entityManager;

        public GetEntityMetadataConsumer(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public async Task Consume(ConsumeContext<GetEntityMetadataRequest> context)
        {
            var data = _entityManager.GetMetadata(context.Message.EntityId, context.Message.Index, context.Message.Type);
            await context.RespondAsync<GetEntityMetadataResponse>(new
            {
                EntityId = context.Message.EntityId, Index = context.Message.Index, Type = context.Message.Type,
                Data = data
            });
        }
    }
}