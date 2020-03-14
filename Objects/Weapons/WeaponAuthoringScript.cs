using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS/Objects/Weapon")]
    public class WeaponAuthoringScript : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public float TimeBetweenShoots = 1.0f;
        public GameObject BulletPrefab;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(BulletPrefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var bulletData = new WeaponComponent
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
