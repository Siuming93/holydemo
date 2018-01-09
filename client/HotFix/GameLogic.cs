using UnityEngine;

namespace HotFix
{
    public class GameLogic
    {
        public static float _lastLogTime;
        public static int _count;
        public static void Tick()
        {
            if (Time.time - _lastLogTime >= 1)
            {
                UnityEngine.Debug.Log(_count++);
                _lastLogTime = Time.time;
            }
        }
    }
}
