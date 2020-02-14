using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    public class RotationSpeedSystem : JobComponentSystem
    {
        private EntityQuery entityQuery;

        protected override void OnCreate()
        {
            entityQuery = GetEntityQuery(typeof(Rotation), ComponentType.ReadOnly<RotationSpeedComponent>());
        }

        [BurstCompile]
        private struct RotationSpeedJob : IJobChunk
        {
            public float DeltaTime;
            public ArchetypeChunkComponentType<Rotation> RotationType;
            [ReadOnly] public ArchetypeChunkComponentType<RotationSpeedComponent> RotationSpeedComponentType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var chunkRotations = chunk.GetNativeArray(RotationType);
                var chunkRotationSpeeds = chunk.GetNativeArray(RotationSpeedComponentType);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var rotation = chunkRotations[i];
                    var rotationSpeed = chunkRotationSpeeds[i];

                    chunkRotations[i] = new Rotation
                    {
                        Value = math.mul(math.normalize(rotation.Value), quaternion.Euler(rotationSpeed.Value * DeltaTime))
                    };
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var rotationType = GetArchetypeChunkComponentType<Rotation>();
            var rotationSpeedType = GetArchetypeChunkComponentType<RotationSpeedComponent>();

            var job = new RotationSpeedJob()
            {
                DeltaTime = Time.DeltaTime,
                RotationType = rotationType,
                RotationSpeedComponentType = rotationSpeedType
            };

            return job.Schedule(entityQuery, inputDeps);
        }
    }
}
