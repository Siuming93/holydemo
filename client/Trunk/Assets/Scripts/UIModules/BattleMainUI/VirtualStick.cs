using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof (RectTransform))]
public class VirtualStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static VirtualStick Instance { get; private set; }
    public float radius = 40.0f;
    public bool isPressed { get; private set; }

    public int Angle;

    public event Action<VirtualStick, Vector2> OnStickMovementStart;
    public event Action<VirtualStick, Vector2> OnJoystickMovement;
    public event Action<VirtualStick> OnStickMovementEnd;
    public event Action OnStickMovement; 

    private RectTransform _selfRect;
    public RectTransform stickRectTransform;
    public RectTransform stickHandlerRectTransform;

    private bool _notifyStart;

    public Vector2 Coordinates
    {
        get;
        private set;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Vector2 pointP;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_selfRect, eventData.position, eventData.enterEventCamera, out pointP);
        this.stickRectTransform.anchoredPosition = pointP;
        isPressed = true;
        _notifyStart = false;
        this.stickRectTransform.gameObject.SetActive(true);
       
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        this.stickRectTransform.gameObject.SetActive(false);
        this.stickHandlerRectTransform.anchoredPosition = Vector2.zero;
        this.Coordinates = Vector2.zero;
        if (OnStickMovementEnd != null)
            OnStickMovementEnd(this);
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var handleOffset = GetJoystickOffset(eventData);
        this.stickHandlerRectTransform.anchoredPosition = handleOffset;
        this.Coordinates = handleOffset.normalized;
        Angle = Mathf.RoundToInt(Mathf.Atan2(Coordinates.y, Coordinates.x) * (180 / Mathf.PI));

        if (handleOffset.sqrMagnitude >= 0.2f && !_notifyStart)
        {
            if (OnStickMovementStart != null)
                OnStickMovementStart(this, Coordinates);
            _notifyStart = true;
            return;
        }

        if (!_notifyStart)
        {
            return;
        }

        if (OnJoystickMovement != null)
        {
            OnJoystickMovement.Invoke(this, Coordinates);
        }
    }

    private Vector2 GetJoystickOffset(PointerEventData eventData)
    {
        //计算位置
        Vector2 pointP;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(stickRectTransform,eventData.position, eventData.enterEventCamera, out pointP);

        //不要出圆
        Vector2 pointO = Vector2.zero;
        Vector2 vectorOP = pointP - pointO;
        float lengthOP = vectorOP.magnitude;
        float factor = lengthOP > radius ? radius/lengthOP : 1f;
        Vector2 pointH = pointO + vectorOP*factor;

        return pointH;
      }

    private void Awake()
    {
        Instance = this;
        _selfRect = GetComponent<RectTransform>();
    }
}