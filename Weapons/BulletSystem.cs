using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    //[UpdateInGroup(typeof(SimulationSystemGroup))]
    //public class BulletSystem : JobComponentSystem
    //{
    //    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    //    protected override void OnCreate()
    //    {
    //        m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    //    }
        
    //    struct SpawnBulletJob : IJobForEachWithEntity<BulletComponent, Translation>
    //    {
    //        public EntityCommandBuffer.Concurrent CommandBuffer;

    //        public void Execute(Entity entity, int index, [ReadOnly] ref BulletComponent bullet, [ReadOnly] ref Translation position)
    //        {
    //            var instance = CommandBuffer.Instantiate(index, bullet.Prefab);
                
    //            CommandBuffer.SetComponent(index, instance, new Translation { Value = position.Value });
    //            CommandBuffer.DestroyEntity(index, entity);
    //        }
    //    }

    //    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    //    {
    //        var job = new SpawnBulletJob
    //        {
    //            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
    //        }.Schedule(this, inputDependencies);

    //        m_EntityCommandBufferSystem.AddJobHandleForProducer(job);

    //        return job;
    //    }
    //}
}
