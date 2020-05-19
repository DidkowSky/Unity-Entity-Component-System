using System;
using Unity.Entities;

namespace Assets.Scripts.DOTS
{
    [Serializable]
    public struct BulletComponent : IComponentData
    {
        public float MinDamage;
        public float MaxDamage;
        public float Range;
    }
}
