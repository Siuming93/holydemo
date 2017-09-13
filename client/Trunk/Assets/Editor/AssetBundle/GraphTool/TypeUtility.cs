using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.AssetBundle.GraphTool
{
    class TypeUtility
    {
        private const string ExtensionPattern = "(.prefab)";

        public List<string> FliterByExtension(List<string> pathList)
        {
            List<string> output = new List<string>();

            foreach (var path in pathList)
            {
                if (Regex.IsMatch(path, ExtensionPattern))
                    output.Add(path);
            }

            return output;
        }

        private static readonly List<Type> IgnoreTypes = new List<Type> {
            typeof(MonoScript),
        };

        public static bool IsLoadingAsset(AssetReference r)
        {
            Type t = r.assetType;
            return t != null && !IgnoreTypes.Contains(t);
        }

        /**
		 * Get type of asset from give path.
		 */
        public static Type GetTypeOfAsset(string assetPath)
        {
            if (assetPath.EndsWith(".Meta"))
            {
                return typeof(string);
            }

            Type t = null;
#if (UNITY_5_4_OR_NEWER && !UNITY_5_4_0 && !UNITY_5_4_1)

            t = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            if (t == typeof(MonoBehaviour))
            {
                UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                t = asset.GetType();
                //Resources.UnloadAsset(asset);
            }

#else

			UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(assetPath);

			if (asset != null) {
				t = asset.GetType();
				if(asset is UnityEngine.GameObject || asset is UnityEngine.Component) {
					// do nothing.
					// NOTE: DestroyImmediate() will destroy persistant GameObject in prefab. Do not call it.
				} else {
					Resources.UnloadAsset(asset);
				}
			}
#endif

            return t;
        }

        /**
     * Get asset filter type from asset path.
     */
        public static Type FindAssetFilterType(string assetPath)
        {
            // check by asset importer type.
            //			if (importer == null) {
            //				LogUtility.Logger.LogWarning(LogUtility.kTag, "Failed to assume assetType of asset. The asset will be ignored: " + assetPath);
            //				return typeof(object);
            //			}

            var importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                var importerType = importer.GetType();
                var importerTypeStr = importerType.ToString();

                switch (importerTypeStr)
                {
                    case "UnityEditor.TextureImporter":
                    case "UnityEditor.ModelImporter":
                    case "UnityEditor.AudioImporter":
#if UNITY_5_6 || UNITY_5_6_OR_NEWER
                case "UnityEditor.VideoClipImporter": 
#endif
                        {
                            return importerType;
                        }
                }
            }

            //// not specific type importer. should determine their type by extension.
            //var extension = Path.GetExtension(assetPath).ToLower();
            //if (FilterTypeBindingByExtension.ContainsKey(extension))
            //{
            //    return FilterTypeBindingByExtension[extension];
            //}

            //if (IgnoredAssetTypeExtension.Contains(extension))
            //{
            //    return null;
            //}

            // unhandled.
            //LogUtility.Logger.LogWarning(LogUtility.kTag, "Unknown file type found:" + extension + "\n. AssetReference:" + assetPath + "\n Assume 'object'.");
            return typeof(object);
        }
    }
}
