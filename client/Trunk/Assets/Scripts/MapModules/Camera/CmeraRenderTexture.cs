using UnityEngine;

namespace Assets.Scripts.View.Camera
{
    public class CmeraRenderTexture : MonoBehaviour
    {
        private RenderTexture rtt;

        /// <summary>
        /// 将相机渲染到renderTexture上
        /// </summary>
        void Awake()
        {
            //设定rtt的depth为24bit;
            rtt = new RenderTexture(1024, 768, 24);
            GetComponent<UnityEngine.Camera>().targetTexture = rtt;
        }
    }
}
