using System.Threading.Tasks;
using MassTransit;

namespace EntityDemo
{
    public sealed class UpdateTransformConsumer
        : IConsumer<UpdateTransform>
    {
        private readonly IEntityManager _entityManager;

        public UpdateTransformConsumer(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        public Task Consume(ConsumeContext<UpdateTransform> context)
        {
            _entityManager.UpdateTransform(context.Message.EntityId, context.Message.Position, context.Message.Velocity);
            return Task.CompletedTask;
        }
    }
}