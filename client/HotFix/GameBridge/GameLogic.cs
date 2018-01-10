using System.Collections;
using HotFix.GameFrame.Common;
using HotFix.GameFrame.PreLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HotFix
{
    public class GameLogic
    {
        public static float _lastLogTime;
        public static int _count;

        public static void Init()
        {
            _lastLogTime = 0;
            _count = 0;

            TickProxy.Instance.Init();
            //TickProxy.Instance.StartCoroutine(TestCoroutine());
            PreloadManager.Instance.Init();
        }

        public static void Tick()
        {
            TickProxy.Instance.Tick();
        }

        public static void Dispose()
        {
            TickProxy.Instance.Dispose();
        }

        public static IEnumerator TestCoroutine()
        {
            while (true)
            {
                if (Time.time - _lastLogTime >= 1)
                {
                    Debug.Log(_count++);
                    _lastLogTime = Time.time;
                }
                //if (_count >= 10)
                //{
                //    TickProxy.Instance.StopAllCoroutine();
                //    SceneManager.LoadScene("Preload");
                //}
                yield return null;
            }
        }
    }
}
