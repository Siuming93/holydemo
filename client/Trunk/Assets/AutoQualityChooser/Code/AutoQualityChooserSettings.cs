namespace net.krej.AutoQualityChooser {
    [System.Serializable] public class AutoQualityChooserSettings {
        /// <summary>
        /// Disable component if user changed quality manually (for example in menu)
        /// </summary>
        public bool disableAfterManualQualityChange = true;

        /// <summary>
        /// On start set quality of application to highest possible.
        /// </summary>
        public bool forceBestQualityOnStart = true;

        /// <summary>
        /// Minimal framerate (if current FPS is lower for too long, quality should decrease immediately)
        /// </summary>
        public float minAcceptableFramerate = 20;

        /// <summary>
        /// If framrate will be lower than minimal for longer than this value,  quality will decrease
        /// </summary>
        public int timeBeforeQualityDowngrade = 5;
    }
}