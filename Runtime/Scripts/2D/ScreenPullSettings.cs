using UnityEngine;

namespace CustomCamera.TwoDimensional
{
    [System.Serializable]
    public struct ScreenShakeAndPullSettings
    {
        public ScreenShakeSettings screenShakeSettings;
        public ScreenPullSettings screenPullSettings;

        public static ScreenShakeAndPullSettings Default = new ScreenShakeAndPullSettings
        {
            screenShakeSettings = ScreenShakeSettings.Default,
            screenPullSettings = ScreenPullSettings.Default,
        };

        public void Play()
        {
            screenShakeSettings.Play();
            screenPullSettings.Play();
        }

        public void Play(Vector2 pullAmount)
        {
            screenShakeSettings.Play();
            screenPullSettings.Play(pullAmount);
        }
    }

    [System.Serializable]
    public struct ScreenPullSettings
    {
        public Vector2 pullAmount;
        public float easeInDuration;
        public DG.Tweening.Ease easeIn;
        public float easeOutDuration;
        public DG.Tweening.Ease easeOut;

        public static ScreenPullSettings Default => new ScreenPullSettings
        {
            pullAmount = Vector2.down / 10,
            easeIn = DG.Tweening.Ease.OutSine,
            easeInDuration = 0.1f,
            easeOut = DG.Tweening.Ease.InOutSine,
            easeOutDuration = 0.3f
        };

        public void Play()
        {
            ((VirtualCamera2D)CameraSwitcher.ActiveCamera).PullCamera(pullAmount, this);
        }

        public void Play(Vector2 pullAmount)
        {
            ((VirtualCamera2D)CameraSwitcher.ActiveCamera).PullCamera(pullAmount, this);
        }
    }
}