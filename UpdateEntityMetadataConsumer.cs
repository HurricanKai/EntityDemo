using System.Threading.Tasks;
using MassTransit;

namespace EntityDemo
{
    public class UpdateEntityMetadataConsumer
    : IConsumer<UpdateEntityMetadata>
    {
        private readonly IEntityManager _entityManager;

        public UpdateEntityMetadataConsumer(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public Task Consume(ConsumeContext<UpdateEntityMetadata> context)
        {
            var msg = context.Message;
            _entityManager.UpdateMetadata(msg.EntityId, msg.Index, msg.Type, msg.Data);
            return Task.CompletedTask;
        }
    }
}