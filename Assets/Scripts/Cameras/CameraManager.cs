using Cinemachine;
using Player;
using Plugins.Tools;
using UnityEngine;

namespace Cameras
{
    public class CameraManager : PersistentSingleton<CameraManager>
    {
        public CinemachineFreeLook playerCamera;

        private const int MIN_CAMERA_ZOOM = 20;
        private const int MAX_CAMERA_ZOOM = 95;
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
        }
    }
}
