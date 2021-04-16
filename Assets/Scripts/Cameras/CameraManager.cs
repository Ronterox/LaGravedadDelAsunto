using Cinemachine;
using Managers;
using Player;
using Plugins.Tools;
using UnityEngine;

namespace Cameras
{
    public class CameraManager : Singleton<CameraManager>
    {
        public CinemachineFreeLook playerCamera;
        public CinemachineVirtualCamera mapCamera;

        public Transform playerTransform;

        private const int MIN_CAMERA_ZOOM = 20;
        private const int MAX_CAMERA_ZOOM = 95;

        private bool m_IsOnMap;

        protected void Start() => LookForPlayerPosition();

        private void LateUpdate() => SetCameraZoom();

        private void SetCameraZoom()
        {
            if (PlayerInput.Instance.IsScrollingUp)
            {
                playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, MIN_CAMERA_ZOOM, PlayerInput.Instance.zoomSpeed);
            }
            else if (PlayerInput.Instance.IsScrollingDown)
            {
                playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, MAX_CAMERA_ZOOM, PlayerInput.Instance.zoomSpeed);
            }

            if (!PlayerInput.Instance.Map || GameManager.Instance.GameIsPaused) return;

            if (m_IsOnMap)
            {
                playerCamera.Priority = 1;
                mapCamera.Priority = 0;
            }
            else
            {
                playerCamera.Priority = 0;
                mapCamera.Priority = 1;
            }
            PlayerController.Instance.BlockMovement(m_IsOnMap = !m_IsOnMap);
        }

        private void LookForPlayerPosition()
        {
            if (!playerTransform) playerTransform = PlayerInput.Instance.transform;

            if (!playerTransform) return;
            playerCamera.Follow = playerTransform;
            playerCamera.LookAt = playerTransform.Find("CameraLookAt");
        }
    }
}
