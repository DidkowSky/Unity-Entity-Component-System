using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Assets.Scripts.DOTS
{
    public class PlayerWeaponsSystem : SystemBase
    {
        private BeginInitializationEntityCommandBufferSystem beginInitializationEntityCommandBuffer;

        protected override void OnCreate()
        {
            beginInitializationEntityCommandBuffer = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var CommandBuffer = beginInitializationEntityCommandBuffer.CreateCommandBuffer().ToConcurrent();
            var deltaTime = Time.DeltaTime;

            Entities
            .WithAll<Translation, PlayerWeaponComponent>()
            .ForEach((Entity entity, int entityInQueryIndex, ref LocalToWorld localToWorld, /*ref Translation translation,*/ ref PlayerWeaponComponent weapon) =>
            {
                if (weapon.isShooting)
                {
                    if (weapon.TimeSinceLastShoot == 0.0f || weapon.TimeSinceLastShoot >= weapon.TimeBetweenShoots)
                    {
                        var newBullet = CommandBuffer.Instantiate(entityInQueryIndex, weapon.BulletPrefab);
                        var bulletComponent = GetComponent<BulletComponent>(weapon.BulletPrefab);

                        bulletComponent.StartingPosition = localToWorld.Position;

                        CommandBuffer.SetComponent(entityInQueryIndex, newBullet, new Translation { Value = localToWorld.Position });
                        CommandBuffer.SetComponent(entityInQueryIndex, newBullet, bulletComponent);

                        if (weapon.TimeSinceLastShoot > 0.0f)
                        {
                            weapon.TimeSinceLastShoot = 0.0f;
                        }
                    }
                }

                if (weapon.TimeSinceLastShoot <= weapon.TimeBetweenShoots)
                {
                    weapon.TimeSinceLastShoot += deltaTime;
                }
            })
            .ScheduleParallel();
        }
    }
}
