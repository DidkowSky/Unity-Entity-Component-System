using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.DOTS
{
    public struct PositionComponent : IComponentData
    {
        public float3 Value;
    }
}
