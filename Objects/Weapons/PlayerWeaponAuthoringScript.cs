using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS/Objects/Player Weapon")]
    public class PlayerWeaponAuthoringScript : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public float TimeBetweenShoots = 1.0f;
        public GameObject BulletPrefab;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(BulletPrefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var bulletData = new PlayerWeaponComponent
            {
                TimeBetweenShoots = TimeBetweenShoots,
                BulletPrefab = conversionSystem.GetPrimaryEntity(BulletPrefab),
                GameObject = conversionSystem.GetPrimaryEntity(gameObject),
                isShooting = false,
                TimeSinceLastShoot = 0.0f
            };

            dstManager.AddComponentData(entity, bulletData);
        }
    }
}
