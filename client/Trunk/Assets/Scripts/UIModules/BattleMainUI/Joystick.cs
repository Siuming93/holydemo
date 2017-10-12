using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof (RectTransform))]
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static Joystick Instance { get; private set; }
    public Vector2 autoReturnSpeed = new Vector2(4.0f, 4.0f);
    public float radius = 40.0f;

    public event Action<Joystick, Vector2> OnStartJoystickMovement;
    public event Action<Joystick, Vector2> OnJoystickMovement;
    public event Action<Joystick> OnEndJoystickMovement;

    private bool _returnHandle;
    private RectTransform _canvas;

    public RectTransform stickRectTransform;
    public RectTransform stickHandlerRectTransform;


    //public Vector2 Coordinates
    //{
    //    get
    //    {
    //        //if (handle.anchoredPosition.magnitude < radius)
    //        //    return handle.anchoredPosition/radius;
    //        //return handle.anchoredPosition.normalized;
    //    }
    //}

    private bool _isPress;
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _isPress = true;
        this.stickRectTransform.gameObject.SetActive(true);
        //if (OnStartJoystickMovement != null)
            //OnStartJoystickMovement(this, Coordinates);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var handleOffset = GetJoystickOffset(eventData);
        //if (OnJoystickMovement != null)
            //OnJoystickMovement(this, Coordinates);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        _isPress = false;
        this.stickRectTransform.gameObject.SetActive(false);
        if (OnEndJoystickMovement != null)
            OnEndJoystickMovement(this);
    }

    private Vector2 GetJoystickOffset(PointerEventData eventData)
    {
        //计算位置
        //Vector3 globalHandle;
        //if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, eventData.position,
        //    eventData.pressEventCamera, out globalHandle))

        ////不要出圆
        
        return Vector2.zero;
      }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _returnHandle = true;
        var touchZone = GetComponent<RectTransform>();

        //设置中心
        touchZone.pivot = Vector2.one*0.5f;
        //设定handle的位置
        //handle.transform.SetParent(transform);

        //找到canvas
        var curTransform = transform;
        do
        {
            if (curTransform.GetComponent<Canvas>() != null)
            {
                _canvas = curTransform.GetComponent<RectTransform>();
                break;
            }
            curTransform = transform.parent;
        } while (curTransform != null);
    }

    private void Update()
    {
        //开始返回,anchoredPosition搞到(0,0)去
        //if (_returnHandle)
        //    if (handle.anchoredPosition.magnitude > Mathf.Epsilon)
        //        handle.anchoredPosition -=
        //            new Vector2(handle.anchoredPosition.x*autoReturnSpeed.x, handle.anchoredPosition.y*autoReturnSpeed.y)*
        //            Time.deltaTime;
        //    else
        //        _returnHandle = false;
    }
}