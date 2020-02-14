using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.DOTS
{
    [Serializable]
    public struct MovementComponent : IComponentData
    {
        public float3 Vector;
        public float Speed;
        public float MaxSpeed;
    }
}
