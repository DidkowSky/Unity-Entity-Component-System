using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS/Objects/Player")]
    public class PlayerAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float3 DestinationPoint;
        public float MoveSpeed;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new PlayerComponent()
            {
                DestinationPoint = DestinationPoint,
                MoveSpeed = MoveSpeed
            };

            dstManager.AddComponentData(entity, data);
        }
    }
}
