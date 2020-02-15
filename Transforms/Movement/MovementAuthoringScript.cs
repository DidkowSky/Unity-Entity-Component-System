using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS/Transform/Movement")]
    public class MovementAuthoringScript : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float3 Vector;
        public float Velocity;
        public float Acceleration;
        public float MaxSpeed;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new MovementComponent
            {
                Vector = Vector,
                Velocity = Velocity,
                Acceleration = Acceleration,
                MaxSpeed = MaxSpeed
            };

            dstManager.AddComponentData(entity, data);
        }
    }
}