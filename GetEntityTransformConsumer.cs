using System.Threading.Tasks;
using MassTransit;

namespace EntityDemo
{
    public class GetEntityTransformConsumer
        : IConsumer<GetEntityTransformRequest>
    {
        private readonly IEntityManager _entityManager;

        public GetEntityTransformConsumer(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public async Task Consume(ConsumeContext<GetEntityTransformRequest> context)
        {
            var (position, velocity) = _entityManager.GetTransform(context.Message.EntityId);
            await context.RespondAsync<GetEntityTransformResponse>(new
                {EntityId = context.Message.EntityId, Position = position, Velocity = velocity});
        }
    }
}