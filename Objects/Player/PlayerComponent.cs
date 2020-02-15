using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.DOTS
{
    [Serializable]
    public struct PlayerComponent : IComponentData
    {
        public float3 DestinationPoint;
        public float MoveSpeed;
    }
}
