using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
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
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    SetPlayersPosition(Input.touches[0]);
                }
                else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    SetPlayersPosition(Input.touches[0]);
                }

                if (Input.touches.Length > 1)
                {
                    if (Input.touches[1].phase == TouchPhase.Began)
                    {
                        Debug.Log("Start Shoot!");
                    }
                    else if (Input.touches[1].phase == TouchPhase.Ended)
                    {
                        Debug.Log("Stop Shooting.");
                    }
                }
            }
        }

        private void SetPlayersPosition(Touch touch)
        {
            Entities.WithAll<PlayerComponent>().ForEach((ref PlayerComponent playerComponent) =>
            {
                playerComponent.DestinationPoint = GetTouchPosition(touch);
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