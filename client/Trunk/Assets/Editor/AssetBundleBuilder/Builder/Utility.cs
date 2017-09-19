using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Editor.AssetBundleBuilder.GraphTool
{
    class Utility
    {
        private static readonly List<string> IgnoredAssetTypeExtension = new List<string>{
            string.Empty,
            ".manifest",
            ".meta",
            ".assetbundle",
            ".sample",
            ".unitypackage",
            ".cs",
            ".sh",
            ".js",
            ".zip",
            ".tar",
            ".tgz",
			#if UNITY_5_6 || UNITY_5_6_OR_NEWER
			#else
			".m4v",
			#endif
		};

        public static bool IsLoadingAsset(string fileExtension)
        {
            return !IgnoredAssetTypeExtension.Contains(fileExtension);
        }

        public static string GetRelativeAssetsPath(string path)
        {
            return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
        }

        public static string GetBundleNames(string path)
        {
            return Path.GetFileNameWithoutExtension(path) + ".bytes";
        }

        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public static string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }

    }
}
