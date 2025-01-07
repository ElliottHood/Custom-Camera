using UnityEngine;
using Cinemachine;
using EffectorValues;

namespace CustomCamera
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public abstract class CustomVirtualCamera : MonoBehaviour
    {
        public CinemachineVirtualCamera VCam { get; private set; }
        public AdditiveEffectorValue ScreenShakeValue = new AdditiveEffectorValue();
        protected CinemachineBasicMultiChannelPerlin perlinNoise;

        protected virtual void Awake()
        {
            VCam = GetComponent<CinemachineVirtualCamera>();
            perlinNoise = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (perlinNoise == null)
            {
                Debug.LogWarning("Make sure to add BasicMultiChannelPerlin noise to your CinemachineVirtualCamera component");
            }

            CameraSwitcher.Register(this);
        }

        protected virtual void Update()
        {
            perlinNoise.m_AmplitudeGain = Mathf.Max(0, ScreenShakeValue.Evaluate());
        }

        public static void ShakeCamera()
        {
            CameraSwitcher.ActiveCamera.ShakeCamera(ScreenShakeSettings.Default);
        }

        public void ShakeCamera(ScreenShakeSettings shakeSettings)
        {
            AnimatedValue inAnimation = new AnimatedValue(0, 1, 1, DG.Tweening.Ease.OutSine);
            AnimatedValue outAnimation = new AnimatedValue(shakeSettings.duration, 1, 1, DG.Tweening.Ease.OutSine);
            ScreenShakeValue.AddTemporaryEffector(new TemporaryEffector(inAnimation, outAnimation, shakeSettings.intensity));
        }
    }
}