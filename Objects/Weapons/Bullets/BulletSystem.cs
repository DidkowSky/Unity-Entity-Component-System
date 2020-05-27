using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    public class BulletSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem endSimulationEntityCommandBuffer;

        protected override void OnCreate()
        {
            endSimulationEntityCommandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var CommandBuffer = endSimulationEntityCommandBuffer.CreateCommandBuffer().ToConcurrent();

            Entities
            .WithAll<Translation, BulletComponent>()
            .ForEach((Entity entity, int entityInQueryIndex, ref Translation translation, in BulletComponent bullet) =>
            {
                if (translation.Value.y > (bullet.StartingPosition.y + bullet.Range))
                {
                    CommandBuffer.DestroyEntity(entityInQueryIndex, entity);
                }
            })
            .ScheduleParallel();
        }
    }
}
