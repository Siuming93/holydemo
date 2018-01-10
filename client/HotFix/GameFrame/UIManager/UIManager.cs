using UnityEngine;

public class UIManager
{
    private static UIManager mInstance;
    public static UIManager Intance {
        get
        {
            return mInstance;
        }
    }

    private Transform mUIRoot;
    private Transform mUnVisabledLayer;
    private Transform mActiveLayer;
    private Transform mUnActiveLayer;

    public UIManager(Transform uiRoot)
    {
        mUIRoot = uiRoot;
        GameObject.DontDestroyOnLoad(mUIRoot);
        mActiveLayer = mUIRoot.FindChild("ActiveLayer");
        mUnActiveLayer = mUIRoot.FindChild("UnActiveLayer");
        mInstance = this;
    }

    /// <summary>
    /// 清除掉屏幕上的所有UI,但并不销毁资源
    /// </summary>
    public void ClearScreen()
    {
        int count = mActiveLayer.childCount;
        for (int i = 0; i < count; i++)
        {
            RemoveChild(mActiveLayer.GetChild(i));
        }
    }

    /// <summary>
    /// 显示在屏幕上
    /// </summary>
    /// <param name="child"></param>
    public void AddChild(Transform child)
    {
        child.SetParent(mActiveLayer);
        child.localScale = Vector3.one;
        (child as RectTransform).sizeDelta = Vector2.zero;
        (child as RectTransform).localPosition = Vector2.zero;
    }

    public void RemoveChild(Transform child)
    {
        child.SetParent(mUnActiveLayer);
    }
}
