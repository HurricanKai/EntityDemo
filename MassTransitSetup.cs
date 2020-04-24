using System;
using MassTransit;
using MassTransit.Context;

namespace EntityDemo
{
    public static class MassTransitSetup
    {
        public static void Setup()
        {
            MessageCorrelation.UseCorrelationId<CreateEntityResponse>(x => x.EntityId);
            MessageCorrelation.UseCorrelationId<UpdateEntityMetadata>(x => x.EntityId);
            MessageCorrelation.UseCorrelationId<GetEntityMetadataRequest>(x => x.EntityId);
            MessageCorrelation.UseCorrelationId<GetEntityMetadataResponse>(x => x.EntityId);
            EndpointConvention.Map<UpdateEntityMetadata>(new Uri("queue:update-entity-metadata"));
        }
    }
}