using UnityEngine;

namespace Assets.Scripts.View.Charcter.Player
{
    /// <summary>
    /// 角色导航
    /// </summary>
    public class PlayerNavigationView : MonoBehaviour
    {
        private NavMeshAgent nav;

        public float MinDistance;

        private void Start()
        {
            nav = transform.GetComponent<NavMeshAgent>();
            nav.stoppingDistance = MinDistance;
        }

        private void Update()
        {
            //到达位置
            if (nav.enabled)
            {
                if (nav.remainingDistance != 0 && nav.remainingDistance < MinDistance)
                {
                    nav.enabled = false;
                    //TaskManager.Instance.OnArriveDestination();
                }
            }
            ///手动移动了,则停止导航
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h != 0f || v != 0f)
            {
                nav.enabled = false;
            }
        }

        /// <summary>
        /// 开始导航
        /// </summary>
        /// <param name="targPos"></param>
        public void SetDestination(Vector3 targPos)
        {
            nav.enabled = true;
            nav.destination = targPos;
        }
    }
}