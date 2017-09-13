using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Editor.AssetBundle.GraphTool
{
    public class LogUtility
    {
        public static readonly string kTag = "AssetBundle";

        private static Logger s_logger;

        public static Logger Logger
        {
            get
            {
                if (s_logger == null)
                {
#if UNITY_2017_1_OR_NEWER
					s_logger = new Logger(Debug.unityLogger.logHandler);
#else
                    s_logger = new Logger(Debug.logger.logHandler);
#endif
                }

                return s_logger;
            }
        }
    }
}
