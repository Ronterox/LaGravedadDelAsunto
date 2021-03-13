using Cinemachine;
using Player;
using UnityEngine;

namespace Cameras
{
    public class CameraManager : MonoBehaviour
    {
        public CinemachineFreeLook playerCamera;

        private const int MIN_CAMERA_ZOOM = 20;
        private const int MAX_CAMERA_ZOOM = 95;
        private void LateUpdate() => SetCameraZoom();

        private void SetCameraZoom()
        {
            if (PlayerInput.Instance.IsScrollingUp)
            {
                playerCamera.m_Lens.FieldOfView -= PlayerInput.Instance.zoomSpeed;
                if (playerCamera.m_Lens.FieldOfView <= MIN_CAMERA_ZOOM) playerCamera.m_Lens.FieldOfView = MIN_CAMERA_ZOOM;
            }
            else if (PlayerInput.Instance.IsScrollingDown)
            {
                playerCamera.m_Lens.FieldOfView += PlayerInput.Instance.zoomSpeed;
                if (playerCamera.m_Lens.FieldOfView >= MAX_CAMERA_ZOOM) playerCamera.m_Lens.FieldOfView = MAX_CAMERA_ZOOM;
            }
        }
    }
}
