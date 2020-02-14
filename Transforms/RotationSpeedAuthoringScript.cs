using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    public class RotationSpeedAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float DegreesPerSecondX = 360;
        public float DegreesPerSecondY = 360;
        public float DegreesPerSecondZ = 360;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new RotationSpeedComponent
            {
                RadiansPerSecondX = math.radians(DegreesPerSecondX),
                RadiansPerSecondY = math.radians(DegreesPerSecondY),
                RadiansPerSecondZ = math.radians(DegreesPerSecondZ)
            };

            dstManager.AddComponentData(entity, data);
        }
    }
}
