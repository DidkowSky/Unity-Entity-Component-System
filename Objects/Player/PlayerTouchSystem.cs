using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.DOTS
{
    public class PlayerTouchSystem : ComponentSystem
    {
        private Camera cameraComponent;

        protected override void OnStartRunning()
        {
            foreach (var camera in Camera.allCameras)
            {
                if (camera.name == "Main Camera")
                {
                    cameraComponent = camera;
                }
            }
        }

        protected override void OnUpdate()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                if (cameraComponent != null)
                {
                    Vector3 worldTouchPosition = cameraComponent.ScreenToWorldPoint(Input.mousePosition);
                    worldTouchPosition.z = 0;
                    SetPlayerPosition(worldTouchPosition);
                }
                else
                {
                    Debug.LogWarning("CameraComponent is null!");
                }

                if (Input.GetMouseButtonDown(1))
                {
                    StartShooting();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    StopShooting();
                }
            }
#endif
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    SetPlayerPosition(Input.touches[0]);
                }
                else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    SetPlayerPosition(Input.touches[0]);
                }
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    StopShooting();
                }

                if (Input.touches.Length > 1)
                {
                    if (Input.touches[1].phase == TouchPhase.Began)
                    {
                        StartShooting();
                    }
                    else if (Input.touches[1].phase == TouchPhase.Ended)
                    {
                        StopShooting();
                    }
                }
            }
        }

        private void SetPlayerPosition(Vector3 position)
        {
            Entities
                .WithAll<PlayerComponent>()
                .ForEach((ref PlayerComponent playerComponent) =>
                {
                    playerComponent.DestinationPoint = position;
                });
        }

        private void SetPlayerPosition(Touch touch)
        {
            Entities
                .WithAll<PlayerComponent>()
                .ForEach((ref PlayerComponent playerComponent) =>
                {
                    playerComponent.DestinationPoint = GetTouchPosition(touch);
                });
        }

        private void StartShooting()
        {
            Entities
                .WithAll<PlayerWeaponComponent>()
                .ForEach((ref PlayerWeaponComponent weaponComponent) =>
                {
                    weaponComponent.isShooting = true;
                });
        }

        private void StopShooting()
        {
            Entities
                .WithAll<PlayerWeaponComponent>()
                .ForEach((ref PlayerWeaponComponent weaponComponent) =>
                {
                    weaponComponent.isShooting = false;
                });
        }

        private float3 GetTouchPosition(Touch touch)
        {
            if (cameraComponent != null)
            {
                Vector3 worldTouchPosition = cameraComponent.ScreenToWorldPoint(touch.position);
                worldTouchPosition.z = 0;
                return new float3(worldTouchPosition.x, worldTouchPosition.y, worldTouchPosition.z);
            }
            else
            {
                Debug.LogWarning("CameraComponent is null!");
            }

            return float3.zero;
        }
    }
}