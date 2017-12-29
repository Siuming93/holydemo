using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Monster.BaseSystem
{
    public static class GameConfig
    {
        public static string BUNDLE_PATH = Application.dataPath + "/Bundles";//Application.dataPath.Replace("Assets", "") + "Bundles";
        public static string REMOTE_PATH = Application.dataPath.Replace("Assets", "") + "Bundles";

        public static string BundleConfigPath = GameConfig.REMOTE_PATH + "/assetbundleConfig.bytes";

        public static string BundleLocalPath = Application.streamingAssetsPath;

        /// <summary>
        /// milisceond
        /// </summary>
        public static long Time
        {
            get { return (int)((UnityEngine.Time.time - lastLocaleTime)*1000) + lastServerTime; }
        }

        public static float Rtt { set; get; }

        public static float lastLocaleTime;
        public static long lastServerTime;
    }
}
