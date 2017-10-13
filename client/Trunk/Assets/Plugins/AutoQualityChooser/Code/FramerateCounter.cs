using net.krej.AutoQualityChooser;
using UnityEngine;
using UnityEngine.Events;
using net.krej.Singleton;

namespace net.krej.FPSCounter {
    public class FramerateCounter : Singleton<FramerateCounter>{
        public float currentFrameRate;
        public bool showFpsInGame;
        public UnityEvent onFramerateCalculated = new UnityEvent();
        private float updateRate = 1.0f;
        private float accum = 0; // FPS accumulated over the interval
        private int frames = 0; // Frames drawn over the interval
        private float timeleft; // Left time for current interval

        private void Update(){
            timeleft -= Time.deltaTime;
            accum += Time.timeScale/Time.deltaTime;
            ++frames;
            if (timeleft <= 0.0) StartNewInterval();
        }

        private void StartNewInterval(){
            currentFrameRate = accum/frames;
            ResetTimeLeft();
            accum = 0.0F;
            frames = 0;
            onFramerateCalculated.Invoke();
        }

        public void ResetTimeLeft(){
            timeleft = 1.0f/updateRate;
        }

        private void OnGUI(){
            if(showFpsInGame) ShowFpsInCorner();
        }

        const int FPS_BOX_WIDTH = 128;
        const int FPS_BOX_HEIGHT = 32;
        private void ShowFpsInCorner(){
            var fpsTextStyle = new GUIStyle(GUI.skin.box) { fontSize = 10, alignment = TextAnchor.MiddleCenter, richText = true };
            var txt = string.Format("<color=white><size=12><B>Auto Quality Chooser</B></size>\n{1}FPS ({0})</color>", QualityChanger.GetCurrentQualityName(), currentFrameRate.ToString("0"));
            GUI.Box(new Rect(0, 0, FPS_BOX_WIDTH, FPS_BOX_HEIGHT), txt, fpsTextStyle);
        }
    }
}