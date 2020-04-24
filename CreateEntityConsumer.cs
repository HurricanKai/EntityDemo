using System.Threading.Tasks;
using MassTransit;

namespace EntityDemo
{
    public class CreateEntityConsumer
        : IConsumer<CreateEntityRequest>
    {
        private readonly IEntityManager _entityManager;
        
        public CreateEntityConsumer(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }
        
        public async Task Consume(ConsumeContext<CreateEntityRequest> context)
        {
            var id = _entityManager.Create(context.Message.TypeId);
            await context.RespondAsync<CreateEntityResponse>(new {EntityId = id});
        }
    }
}