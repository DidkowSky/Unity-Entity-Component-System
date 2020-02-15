using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    public class PlayerSystem : JobComponentSystem
    {
        private EntityQuery entityQuery;

        protected override void OnCreate()
        {
            entityQuery = GetEntityQuery(typeof(Translation), ComponentType.ReadOnly<PlayerComponent>(), ComponentType.ReadOnly<MovementComponent>());
        }

        [BurstCompile]
        private struct PlayerJob : IJobChunk
        {
            public float DeltaTime;
            public ArchetypeChunkComponentType<Translation> TranslationType;
            [ReadOnly] public ArchetypeChunkComponentType<PlayerComponent> PlayerComponentType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var chunkTranslations = chunk.GetNativeArray(TranslationType);
                var chunkPlayers = chunk.GetNativeArray(PlayerComponentType);

                for (int i = 0; i < chunk.Count; i++)
                {
                    var translation = chunkTranslations[i];
                    var player = chunkPlayers[i];

                    if (translation.Value.x != player.DestinationPoint.x || translation.Value.y != player.DestinationPoint.y || translation.Value.z != player.DestinationPoint.z)
                    {
                        chunkTranslations[i] = new Translation
                        {
                            Value = math.lerp(translation.Value, player.DestinationPoint, player.MoveSpeed * DeltaTime)
                        };
                    }
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var translationType = GetArchetypeChunkComponentType<Translation>();
            var playerType = GetArchetypeChunkComponentType<PlayerComponent>();

            var job = new PlayerJob()
            {
                DeltaTime = Time.DeltaTime,
                TranslationType = translationType,
                PlayerComponentType = playerType,
            };

            return job.Schedule(entityQuery, inputDeps);
        }
    }
}
