using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS/Transform/Rotation Speed")]
    public class RotationSpeedAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float3 Value;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new RotationSpeedComponent
            {
                Value = Value
            };

            dstManager.AddComponentData(entity, data);
        }
    }
}
