using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    public class RotationSpeedSystem : JobComponentSystem
    {
        [BurstCompile]
        struct RotationSpeedJob : IJobForEach<Rotation, RotationSpeedComponent>
        {
            public float DeltaTime;
            
            public void Execute(ref Rotation rotation, [ReadOnly] ref RotationSpeedComponent rotationSpeed)
            {
                rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), rotationSpeed.RadiansPerSecondY * DeltaTime));
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new RotationSpeedJob
            {
                DeltaTime = Time.DeltaTime
            };

            return job.Schedule(this, inputDependencies);
        }
    }
}
