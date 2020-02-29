using System;
using Unity.Entities;

namespace Assets.Scripts.DOTS
{
    [Serializable]
    public struct WeaponComponent : IComponentData
    {
        public float ReloadTime;
        public Entity BulletPrefab;

        public Entity GameObject;
        public bool Shoot;
        public float LastShootTime;
    }
}
