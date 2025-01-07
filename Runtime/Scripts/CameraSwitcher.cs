using System.Collections.Generic;
using UnityEngine;

namespace CustomCamera
{
    public static class CameraSwitcher
    {
        public static CustomVirtualCamera ActiveCamera = null;
        private static HashSet<CustomVirtualCamera> cameras = new HashSet<CustomVirtualCamera>();
        private const int ACTIVE_CAM_PRIORITY = 100;
        private const int INACTIVE_CAM_PRIORITY = -100;

        public static void SwitchCamera(CustomVirtualCamera camera)
        {
            ActiveCamera = camera;

            camera.VCam.Priority = ACTIVE_CAM_PRIORITY;
            camera.enabled = true;

            foreach (CustomVirtualCamera c in cameras)
            {
                if (c != camera)
                {
                    c.VCam.Priority = INACTIVE_CAM_PRIORITY;
                    c.enabled = false;
                }
            }
        }

        public static void Register(CustomVirtualCamera camera)
        {
            if (!cameras.Contains(camera))
            {
                cameras.Add(camera);
            }
        }
    }
}
