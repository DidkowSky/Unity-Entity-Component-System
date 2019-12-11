using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class PositionSystem : JobComponentSystem
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()
        {
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }
        
        struct SetPositionJob : IJobForEachWithEntity<Position>
        {
            public EntityCommandBuffer.Concurrent CommandBuffer;

            public void Execute(Entity entity, int index, [ReadOnly] ref Position position)
            {                
                CommandBuffer.SetComponent(index, entity, new Translation { Value = position.Value });
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new SetPositionJob{ };

            return job.Schedule(this, inputDependencies);
        }
    }
}
