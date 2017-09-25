using UnityEngine;
using UnityEngine.Events;
using net.krej.Singleton;
using net.krej.FPSCounter;

namespace net.krej.AutoQualityChooser {
    [RequireComponent(typeof(FramerateCounter))]
    public class AutoQualityChooser : Singleton<AutoQualityChooser> {
        public UnityEvent onQualityChange;
        public AutoQualityChooserSettings settings = new AutoQualityChooserSettings();
        private FramerateCounter framerateCounter;

        public int secondsBeforeDecreasingQuality = 5;
        private readonly QualityChanger qualityChanger = new QualityChanger();

        private void Start() {
            framerateCounter = GetComponent<FramerateCounter>();
            if (settings.forceBestQualityOnStart) qualityChanger.SetQuality(QualitySettings.names.Length - 1);
            framerateCounter.ResetTimeLeft();
            framerateCounter.onFramerateCalculated.AddListener(OnFramerateUpdated);
            ResetQualityDowngradeTimer();
        }

        private void ResetQualityDowngradeTimer(){
            secondsBeforeDecreasingQuality = settings.timeBeforeQualityDowngrade;
        }

        private void OnFramerateUpdated() {
            if(!enabled)return;
            if (IsFramerateTooLow()) secondsBeforeDecreasingQuality--;
            else ResetQualityDowngradeTimer();
            if (secondsBeforeDecreasingQuality < 0) DecreaseQuality();
        }

        private bool IsFramerateTooLow(){
            return framerateCounter.currentFrameRate < settings.minAcceptableFramerate;
        }

        private void DecreaseQuality(){
            qualityChanger.DecreaseQuality();
            ResetQualityDowngradeTimer();
        }
    }
}
