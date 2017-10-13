using UnityEngine;
using net.krej.AutoQualityChooser;
using net.krej.FPSCounter;

public class ParticleAmountIncreaser : MonoBehaviour {
    private ParticleSystem particles;

    void Start() {
        particles = GetComponent<ParticleSystem>();
        particles.emissionRate = 0;

    }

    void Update() {
        if (FramerateCounter.Instance.currentFrameRate >= 0.9 * AutoQualityChooser.Instance.settings.minAcceptableFramerate)
            particles.emissionRate += Time.deltaTime * 10;
    }
}
