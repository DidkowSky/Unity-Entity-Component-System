using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    public class Boot
    {
        private const int StartEntitiesCount = 10; // how many entities we'll spawn on scene start
        public static EntityArchetype Archetype1 { get; private set; }
        public static RenderMesh EntityLook { get; private set; }

        private static EntityManager entityManager;

        // This attribute allows us to not use MonoBehaviours to instantiate entities
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeBeforeScene()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager; // EntityManager manages all entities in world
            CreateArchetypes(entityManager); // we need to set archetype first
        }

        // This attribute allows us to not use MonoBehaviours to instantiate entities
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public void InitializeAfterScene()
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager; // EntityManager manages all entities in world
            EntityLook = GameObject.Find("SpaceShip").GetComponent<RenderMesh>(); // no other graphic api for now (06.2018)
            CreateEntities(entityManager, StartEntitiesCount);//CreateEntities(em, StartEntitiesCount);
            //em.CreateEntity( new PositionComponent { Value = new float3(0, 0, 0) } );
        }

        private static void CreateArchetypes(EntityManager entityManager)
        {
            // ComponentType.Create<> is slightly more efficient than using typeof()
            // em.CreateArchetype(typeof(Position), typeof(Heading), typeof(Health), typeof(MoveSpeed));
            var pos = new ComponentType(typeof(PositionComponent));
            var bullet = new ComponentType(typeof(BulletComponent));
            var movement = new ComponentType(typeof(MovementComponent));

            Archetype1 = entityManager.CreateArchetype(pos, bullet, movement);
            // that's exactly how you set your entities archetype, it's like LEGO
        }

        private static void CreateEntities(EntityManager entityManager, int count)
        {
            // if you spawn more entities, it's more performant to do it with NativeArray
            // if you want to spawn just one entity, do:
            // var entity = em.CreateEntity(archetype1);
            NativeArray<Entity> entities = new NativeArray<Entity>(count, Allocator.Temp);
            entityManager.CreateEntity(Archetype1, entities); // Spawns entities and attach to them all components from archetype1

            // If we don't set components, their values will be default
            for (int i = 0; i < count; i++)
            {
                // Heading is build in Unity component and you need to set it
                // because default is float3(0, 0, 0), which is position
                // where you can't look towards, so you'll get error from TransformSystem.
                entityManager.SetComponentData(entities[i], new BulletComponent() { Damage = 1 });
                entityManager.SetComponentData(entities[i], new PositionComponent() { Value = new float3(0, 4, 0) });
                entityManager.SetComponentData(entities[i], new MovementComponent() { Vector = new float3(0, 1, 0), Speed = 1, MaxSpeed = 2 });
            }
            entities.Dispose(); // all NativeArrays you need to dispose manually, it won't destroy our entities, just dispose not used anymore array
                                // that's it, entities exists in world and are ready to be injected into systems
        }
    }
}