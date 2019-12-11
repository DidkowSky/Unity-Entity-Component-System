using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    public class BulletAuthoringScript: MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject Prefab;

        private static EntityManager entityManager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initalize()
        {
            entityManager = World.Active.EntityManager;
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(Prefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var bullet = new Bullet
            {
                Prefab = conversionSystem.GetPrimaryEntity(Prefab),
                MovementVector = float3.zero,
                Damage = 1
            };
            /*
            var position = new Position
            {
                Value = new float3(0, 5, 0)
            };
            */
            dstManager.AddComponentData(entity, bullet);
            //dstManager.AddComponentData(entity, position);
        }
    }
}
