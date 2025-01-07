using Cinemachine;
using EffectorValues;
using UnityEngine;

namespace CustomCamera.TwoDimensional
{
    public class VirtualCamera2D : CustomVirtualCamera
    {
        public AdditiveEffectorValue ScreenPullValueX = new AdditiveEffectorValue();
        public AdditiveEffectorValue ScreenPullValueY = new AdditiveEffectorValue();
        public Transform Target => transposer.FollowTarget;
        protected CinemachineTransposer transposer;

        protected override void Awake()
        {
            base.Awake();

            transposer = VCam.GetCinemachineComponent<CinemachineTransposer>();
            if (perlinNoise == null)
            {
                Debug.LogWarning("No Transposer on the 2D virtual camera: Screen pull effect will not work");
            }
        }

        protected override void Update()
        {
            base.Update();
            transposer.m_FollowOffset = new Vector3(ScreenPullValueX.Evaluate(), ScreenPullValueY.Evaluate(), transposer.m_FollowOffset.z);
        }

        public void PullCamera(Vector2 pullAmount, ScreenPullSettings pullSettings)
        {
            AnimatedValue inAnimation = new AnimatedValue(pullSettings.easeInDuration, 1, 1, pullSettings.easeIn);
            AnimatedValue outAnimation = new AnimatedValue(pullSettings.easeOutDuration, 1, 1, pullSettings.easeOut);
            var tempEffector = new TemporaryEffector(inAnimation, outAnimation, pullAmount.x);

            ScreenPullValueX.AddTemporaryEffector(tempEffector);
            tempEffector.Intensity = pullAmount.y;
            ScreenPullValueY.AddTemporaryEffector(tempEffector);
        }
    }
}