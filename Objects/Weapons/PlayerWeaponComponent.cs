using System;
using Unity.Entities;

namespace Assets.Scripts.DOTS
{
    [Serializable]
    public struct PlayerWeaponComponent : IComponentData
    {
        public float TimeBetweenShoots;
        public Entity BulletPrefab;

        public Entity GameObject;
        public bool isShooting;
        public float TimeSinceLastShoot;
        //public Prefab WeaponPrefab;
    }
}
