using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.AssetBundleBuilder
{
    [System.Serializable]
    class AssetBundleDB : ScriptableObject
    {
        public BuilderConfig config;
    }
}
