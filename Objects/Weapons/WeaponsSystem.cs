using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class WeaponsSystem : JobComponentSystem
    {
        private BeginInitializationEntityCommandBufferSystem entityCommandBufferSystem;
        private EntityQuery entityQuery;

        protected override void OnCreate()
        {
            entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
            entityQuery = GetEntityQuery(typeof(Translation), ComponentType.ReadOnly<WeaponComponent>());
        }

        [BurstCompile]
        private struct ShootJob : IJobChunk
        {
            public float Time;
            [ReadOnly] public ArchetypeChunkComponentType<Translation> TranslationType;
            public ArchetypeChunkComponentType<WeaponComponent> WeaponComponentType;
            [WriteOnly] public EntityCommandBuffer.Concurrent CommandBuffer;

            [ReadOnly] [NativeSetThreadIndex] private int threadIndex;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var chunkTranslations = chunk.GetNativeArray(TranslationType);
                var chunkWeapons = chunk.GetNativeArray(WeaponComponentType);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var translation = chunkTranslations[i];
                    var weapon = chunkWeapons[i];

                    if (weapon.Shoot && ((Time - weapon.LastShootTime) >= weapon.ReloadTime))
                    {
                        weapon.LastShootTime = Time;
                        var newBullet = CommandBuffer.Instantiate(threadIndex, weapon.BulletPrefab);
                        CommandBuffer.SetComponent(threadIndex, newBullet, new Translation { Value = translation.Value });
                    }
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var translationType = GetArchetypeChunkComponentType<Translation>();
            var weaponType = GetArchetypeChunkComponentType<WeaponComponent>();

            var commandBuffer = entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();

            var job = new ShootJob()
            {
                Time = UnityEngine.Time.time,
                TranslationType = translationType,
                WeaponComponentType = weaponType,
                CommandBuffer = commandBuffer
            };

            return job.Schedule(entityQuery, inputDeps);
        }
    }
}
