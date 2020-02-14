using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.DOTS
{
    [Serializable]
    public struct PositionComponent : IComponentData
    {
        public float3 Value;
    }
}
