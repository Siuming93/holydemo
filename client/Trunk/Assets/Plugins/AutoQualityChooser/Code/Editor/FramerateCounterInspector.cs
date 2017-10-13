using UnityEngine;
using UnityEditor;
using net.krej.FPSCounter;

namespace net.krej.AutoQualityChooser{
    [CustomEditor(typeof (FramerateCounter))] public class FramerateCounterEditor : Editor{
        private FramerateCounter mTarget;

        public void OnEnable(){
            mTarget = target as FramerateCounter;
        }

        public override void OnInspectorGUI(){
            if (EditorApplication.isPlaying)
                DrawPlayModeInspector();
            ShowCheckboxForShowingFpsInGame();
        }

        private void DrawPlayModeInspector(){
            GUILayout.Label(string.Format("Current FPS: {0}", mTarget.currentFrameRate.ToString("0.0"))); 
        }

        private void ShowCheckboxForShowingFpsInGame(){
            var newValueOfShowFpsInGame = GUILayout.Toggle(mTarget.showFpsInGame, "Show quality and framerate in game");
            if (newValueOfShowFpsInGame != mTarget.showFpsInGame){
                Undo.RecordObject(mTarget, "Showing quality and framerate in game " + (newValueOfShowFpsInGame?"enabled":"disabled"));
                mTarget.showFpsInGame = newValueOfShowFpsInGame;
            }
        }
    }
}