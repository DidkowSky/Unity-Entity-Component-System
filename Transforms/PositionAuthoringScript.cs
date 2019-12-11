using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    public class PositionAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Vector3 Value = Vector3.zero;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Position
            {
                Value = new float3(Value.x, Value.y, Value.z)
            };

            dstManager.AddComponentData(entity, data);
        }
    }
}
