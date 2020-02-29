using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    public class MovementSystem : JobComponentSystem
    {
        private EntityQuery entityQuery;

        protected override void OnCreate()
        {
            entityQuery = GetEntityQuery(typeof(Translation), ComponentType.ReadOnly<MovementComponent>());
        }

        [BurstCompile]
        private struct MovementJob : IJobChunk
        {
            public float DeltaTime;
            public ArchetypeChunkComponentType<Translation> TranslationType;
            public ArchetypeChunkComponentType<MovementComponent> MovementComponentType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var chunkTranslations = chunk.GetNativeArray(TranslationType);
                var chunkMovements = chunk.GetNativeArray(MovementComponentType);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var translation = chunkTranslations[i];
                    var movement = chunkMovements[i];

                    chunkTranslations[i] = new Translation
                    {
                        Value = translation.Value + (movement.Vector * movement.Velocity * DeltaTime)
                    };
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var translationType = GetArchetypeChunkComponentType<Translation>();
            var movementType = GetArchetypeChunkComponentType<MovementComponent>();

            var job = new MovementJob()
            {
                DeltaTime = Time.DeltaTime,
                TranslationType = translationType,
                MovementComponentType = movementType
            };

            return job.Schedule(entityQuery, inputDeps);
        }
    }
}
