using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.DOTS
{
    public struct MovementComponent : IComponentData
    {
        public float3 Vector;
        public float Speed;
        public float MaxSpeed;
    }
}
