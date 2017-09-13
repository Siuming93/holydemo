using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Editor.AssetBundle.GraphTool
{
    class AssetReference
    {
        protected AssetReference()
        { }
        public string fullName;

        public string fileName;

        public string fileNameWithExtesion;

        public string version;

        public Type assetType;

        public static AssetReference Create(string path)
        {

            return new AssetReference();
        }
    }
}
