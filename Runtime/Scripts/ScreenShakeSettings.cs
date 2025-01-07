namespace CustomCamera
{
    [System.Serializable]
    public struct ScreenShakeSettings
    {
        public float intensity;
        public float duration;

        public static ScreenShakeSettings Default => new ScreenShakeSettings
        {
            intensity = 0.3f,
            duration = 0.3f
        };

        public void Play()
        {
            CameraSwitcher.ActiveCamera.ShakeCamera(this);
        }
    }
}