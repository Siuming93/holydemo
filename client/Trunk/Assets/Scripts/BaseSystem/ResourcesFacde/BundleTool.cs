﻿using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using UnityEngine;

namespace Monster.BaseSystem
{
    public class BundleTool
    {
    }

    [Serializable]
    public class AssetRefrenceNode
    {
        public List<string> depenceOnMe = new List<string>();
        public List<string> depence = new List<string>();
        public string path;
        public string assetName;
        public List<string> incluedDepReference = new List<string>();
    }
}
