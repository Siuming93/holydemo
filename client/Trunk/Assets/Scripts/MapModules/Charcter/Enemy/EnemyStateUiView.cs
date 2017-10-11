using System.Collections;
using System.Reflection.Emit;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View.Charcter.Enemy
{
    /// <summary>
    /// 负责动画的播放,血量的显示
    /// </summary>
    public class EnemyStateUiView : MonoBehaviour
    {
        ///// <summary>
        ///// 绑定一个的状态
        ///// </summary>
        //public EnemyState EnemyState;

        ///// <summary>
        ///// UI中放血条和名称的父Tranform
        ///// </summary>
        //public Transform HpandNameUiParenTransform;

        ///// <summary>
        ///// 血条和名称的Prefab
        ///// </summary>
        //public GameObject HpandNameUiPerfab;

        ///// <summary>
        ///// 受伤时的出血特效
        ///// </summary>
        //public GameObject BloodSplatEffectPerfab;

        ///// <summary>
        ///// 死亡消失时间
        ///// </summary>
        //public float DisapearTime;

        ///// <summary>
        ///// 出血特效的位置
        ///// </summary>
        //public Vector3 BooldEffectVector3;

        //private Animator _animator;
        //private HpandNameUiView _hpandNameUiView;
        //private AudioSource _audioSource;
        //private bool _isDying = false;

        //private static int count = 0;

        //private void Awake()
        //{
        //    //添加状态改变事件监视
        //    EnemyState.OnTakeDamageEvent += OnTakeDamage;
        //    EnemyState.OnInfoChangeEvent += OnUpdateShowInfo;

        //    //找到血条的父object
        //    HpandNameUiParenTransform = GameObject.Find("HpandNamePanel").transform;

        //    //在预制中找到hp和nameui类,并设定值
        //    _hpandNameUiView =
        //        ((GameObject) Instantiate(HpandNameUiPerfab)).GetComponent<HpandNameUiView>();
        //    _hpandNameUiView.transform.parent = HpandNameUiParenTransform;
        //    _hpandNameUiView.Fellow = transform;
        //    _hpandNameUiView.Name = EnemyState.Name;

        //    //初始化动画控制
        //    _animator = transform.GetComponentInChildren<Animator>();
        //    _audioSource = transform.GetComponent<AudioSource>();
        //}

        //private void Start()
        //{
        //}


        //private void Init(EnemyState state)
        //{
        //    EnemyState = state;
        //    OnUpdateShowInfo();
        //}


        //private void PlayAnimation()
        //{
        //    //把所有关于动画的控制挪到这里来
        //}

        //private void OnUpdateShowInfo()
        //{
        //    _hpandNameUiView.HpPercent = EnemyState.GetHpPercet();
        //}

        ///// <summary>
        ///// 受到伤害后的响应
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="trigger"></param>
        //private void OnTakeDamage(GameObject source, string trigger)
        //{
        //    if (_isDying)
        //        return;

        //    //1.播放动画
        //    _animator.SetTrigger(trigger);
        //    //2.击退
        //    transform.position += (transform.position - source.transform.position).normalized;
        //    //3.播放音效,要限制播放数,不超过四个
        //    if (count < 1)
        //    {
        //        _audioSource.Play();
        //        count++;
        //    }
        //    //4.放血
        //    var effect = Instantiate(BloodSplatEffectPerfab, transform.position, transform.rotation) as GameObject;
        //    effect.transform.parent = transform;
        //    effect.transform.localPosition += BooldEffectVector3;

        //    //若死亡,要挪到底下去
        //    if (trigger == "Death")
        //    {
        //        _isDying = true;
        //        StartCoroutine(Dead());
        //    }
        //}

        //private void LateUpdate()
        //{
        //    //播放声音数清零
        //    count = 0;
        //}

        ///// <summary>
        ///// 延时destroy
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerator Dead()
        //{
        //    float timer = 0;
        //    _hpandNameUiView.DestroySelf();

        //    while (timer < DisapearTime)
        //    {
        //        //取消刚体
        //        if (GetComponent<Rigidbody>() != null)
        //            Destroy(GetComponent<Rigidbody>());
        //        var cc = GetComponent<CharacterController>();
        //        if (cc != null)
        //            cc.enabled = false;
        //        timer += Time.deltaTime;
        //        transform.position -= 3*transform.up*Time.deltaTime;
        //        yield return null;
        //    }

        //    Destroy(gameObject);
        //}
    }
}