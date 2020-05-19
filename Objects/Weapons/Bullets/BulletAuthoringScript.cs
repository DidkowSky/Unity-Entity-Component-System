using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS/Objects/Bullet")]
    public class BulletAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float MinDamage = 0.0f;
        public float MaxDamage = 10.0f;
        public float Range = 5.0f;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var bulletData = new BulletComponent
            {
                MinDamage = MinDamage,
                MaxDamage = MaxDamage,
                Range = Range
            };

            dstManager.AddComponentData(entity, bulletData);
        }
    }
}
