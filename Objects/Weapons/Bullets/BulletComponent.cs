using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.DOTS
{
    [Serializable]
    public struct BulletComponent : IComponentData
    {
        public float MinDamage;
        public float MaxDamage;
        public float Range;
        public float3 StartingPosition;
    }
}
