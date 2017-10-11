using UnityEngine;

namespace Assets.Scripts.View.Charcter.Player
{
    /// <summary>
    /// 控制角色动画
    /// </summary>
    public class AnimationView : MonoBehaviour
    {
        public bool IsPlayer;

        /// <summary>
        /// 感应值
        /// </summary>
        public float sensitive;

        /// <summary>
        /// 导航组件
        /// </summary>
        private NavMeshAgent nav;

        /// <summary>
        /// 动画播放器
        /// </summary>
        private Animator animator;

        /// <summary>
        /// 刚体
        /// </summary>
        private Rigidbody rigidbody;

        /// <summary>
        /// 
        /// </summary>
        private CharacterController cc;

        public void PlayAnimation(string trigger)
        {
            animator.SetTrigger(trigger);

            animator.SetBool(trigger, true);
        }

        private void Start()
        {
            nav = transform.GetComponentInParent<NavMeshAgent>();
            rigidbody = transform.GetComponentInParent<Rigidbody>();
            animator = transform.GetComponentInChildren<Animator>();
            cc = transform.GetComponentInChildren<CharacterController>();
        }

        private void Update()
        {
            if (IsPlayer)
            {
                //若正在导航或者刚体有速度,则播放移动动画
                if (nav.enabled || rigidbody.velocity.magnitude > sensitive)
                    animator.SetBool("IsMove", true);
                else
                    animator.SetBool("IsMove", false);
            }
        }
    }
}