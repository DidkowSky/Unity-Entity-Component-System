using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    public class BulletSystem : JobComponentSystem
    {
        private EntityCommandBufferSystem entityCommandBufferSystem;

        protected override void OnCreate()
        {
            entityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        [BurstCompile]
        private struct BulletJob : IJobForEachWithEntity<BulletComponent, Translation>
        {
            public float DeltaTime;

            [WriteOnly] public EntityCommandBuffer.Concurrent CommandBuffer;

            public void Execute(Entity entity, int jobIndex, ref BulletComponent lifeTime, ref Translation translation)
            {
                if (translation.Value.y > 8)
                {
                    CommandBuffer.DestroyEntity(jobIndex, entity);
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var commandBuffer = entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();

            var job = new BulletJob
            {
                DeltaTime = Time.DeltaTime,
                CommandBuffer = commandBuffer
            }.Schedule(this, inputDependencies);

            entityCommandBufferSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}
