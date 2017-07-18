using System;
using UnityEngine;
using System.Collections;

public class UIManager
{
    private static UIManager mInstance;
    public static UIManager Intance {
        get
        {
            if (mInstance == null)
            {
               throw new Exception("找不到UIRoot,请先创建一个实例");
            }
            return mInstance;
        }
    }

    private Transform mUIRoot;
    private Transform mUnVisabledLayer;
    private Transform mActiveLayer;

    public UIManager(Transform uiRoot)
    {
        mUIRoot = uiRoot;
        GameObject.DontDestroyOnLoad(mUIRoot);
        mActiveLayer = mUIRoot.FindChild("ActiveLayer");
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
}
