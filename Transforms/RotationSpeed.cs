using Unity.Entities;

namespace Assets.Scripts.DOTS
{
    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecondX;
        public float RadiansPerSecondY;
        public float RadiansPerSecondZ;
    }
}
