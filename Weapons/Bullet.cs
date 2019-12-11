using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.DOTS
{
    public struct Bullet : IComponentData
    {
        public Entity Prefab;
        public float3 MovementVector;
        public float Damage;
    }
}