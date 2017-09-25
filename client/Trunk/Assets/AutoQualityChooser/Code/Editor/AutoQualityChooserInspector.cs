using UnityEngine;
using UnityEditor;

namespace net.krej.AutoQualityChooser {
    [CustomEditor(typeof(AutoQualityChooser))]
    public class AutoQualityChooserEditor : Editor {
        private AutoQualityChooser mTarget;
        private AutoQualityChooserSettings settings;

        void OnEnable() {
            mTarget = target as AutoQualityChooser;
            settings = mTarget.settings;
        }

        public override void OnInspectorGUI() {
            ShowCurrentQuality();
            GUILayout.Label(string.Format("If framerate will be lower than {0}FPS for {1} seconds,\nquality will decrease.", settings.minAcceptableFramerate, settings.timeBeforeQualityDowngrade), EditorStyles.objectFieldThumb);
            DrawExpandableSettingsInspector();
            if(Application.isPlaying)DrawPlayModeInspector();
            EditorUtility.SetDirty(mTarget);
        }

        private void ShowCurrentQuality() {
            GUILayout.Label(QualityChanger.GetCurrentQualityName());
        }

        private void DrawPlayModeInspector(){
            GUILayout.Label(string.Format("Time with low FPS: {0}/{1}s", settings.timeBeforeQualityDowngrade - mTarget.secondsBeforeDecreasingQuality, settings.timeBeforeQualityDowngrade));
        }

        private static bool settingsOpen;
        private void DrawExpandableSettingsInspector() {
            settingsOpen = EditorGUILayout.Foldout(settingsOpen, "Settings");
            if (settingsOpen) DrawSettingsInspector();
        }

        private void DrawSettingsInspector() {
            DrawMinimalFramerateSlider();
            DrawTimeBeforeQualityDowngradeSlider();
            DrawForceBestQualityOnStartToggle();
            DrawDisableAfterManualQualityChangeToggle();
            DrawOnQualityChangeEventField();
        }

        private void DrawMinimalFramerateSlider() {
            var newFps = EditorGUILayout.IntSlider("Min. FPS", Mathf.RoundToInt(settings.minAcceptableFramerate), 2, 50);
            if (newFps != Mathf.RoundToInt(settings.minAcceptableFramerate)) {
                Undo.RecordObject(mTarget, "Changed minimal acceptable framerate");
                settings.minAcceptableFramerate = newFps;
            }
        }

        private void DrawTimeBeforeQualityDowngradeSlider() {
            var newTime = EditorGUILayout.IntSlider("Max low FPS time", settings.timeBeforeQualityDowngrade, 2, 30);
            if (newTime != settings.timeBeforeQualityDowngrade) {
                Undo.RecordObject(mTarget, "Changed maximal acceptable time with FPS lower than min.");
                settings.timeBeforeQualityDowngrade = newTime;
            }
        }

        private void DrawDisableAfterManualQualityChangeToggle() {
            var newToggleValue = GUILayout.Toggle(settings.disableAfterManualQualityChange, "Disable after manual quality change");
            if (newToggleValue != settings.disableAfterManualQualityChange) {
                Undo.RecordObject(mTarget, "Changed diasble after manual quality change toggle");
                settings.disableAfterManualQualityChange = newToggleValue;
            }
        }

        private void DrawForceBestQualityOnStartToggle() {
            var newToggleValue = GUILayout.Toggle(settings.forceBestQualityOnStart, "Force best quality on start");
            if (settings.forceBestQualityOnStart != newToggleValue) {
                Undo.RecordObject(mTarget, "Changed force best quality on start toggle");
                settings.forceBestQualityOnStart = newToggleValue;
            }
        }

        private void DrawOnQualityChangeEventField() {
            serializedObject.Update();
            SerializedProperty prop = serializedObject.FindProperty("onQualityChange");
            EditorGUILayout.PropertyField(prop);
            serializedObject.ApplyModifiedProperties();
        }
    }
}