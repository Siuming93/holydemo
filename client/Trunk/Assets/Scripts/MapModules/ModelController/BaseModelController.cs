using UnityEngine;

public abstract class BaseModelController
{
    public GameObject model { protected set; get; }

    public Transform root { protected set; get; }

    public RectTransform uiRoot { protected set; get; }

}
