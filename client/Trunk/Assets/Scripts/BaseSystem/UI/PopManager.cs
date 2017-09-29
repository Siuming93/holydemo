using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Monster.BaseSystem.UI
{
    public class PopManager
    {
        private static PopManager _instance;

        public static PopManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PopManager();
                }
                return _instance;
            }
        }

        public enum BackGroundType
        {
            Transparent = 0,
            BlackMask = 1,
            TextureBg = 2,
        }

        public enum AnimatinType
        {
            None = 0,
            Expand = 1,
            FlyIn = 2,
        }

        protected class ViewShowStyle
        {
            public IPopView view;

            public BackGroundType bgType;

            public AnimatinType animationType;
        }

        protected UIManager uiManager;

        protected Dictionary<string, ViewShowStyle>  viewDict  = new Dictionary<string, ViewShowStyle>();
        protected Stack<KeyValuePair<IPopView, IPopContext>> openStack = new Stack<KeyValuePair<IPopView, IPopContext>>(); 

        public void RegisterView(string name, IPopView view, BackGroundType backGroundType, AnimatinType animatinType)
        {
            viewDict[name] = new ViewShowStyle() {view = view, bgType = backGroundType, animationType = animatinType};
        }
        public void UnRegisterView(string name)
        {
            if (viewDict.ContainsKey(name))
            {
                viewDict.Remove(name);
            }
        }
        public void RemoveAllView()
        {
            uiManager.ClearScreen();
            openStack.Clear();
        }

        public void OpenNextView(string viewName, IPopContext context = null, bool hideSelf = true)
        {
            ViewShowStyle viewShowStyle;
            if (!viewDict.TryGetValue(viewName, out viewShowStyle))
            {
                return;
            }

            var view = viewShowStyle.view;
            string error = "";
            if (!view.CheckCanShow(context, ref error))
            {
                return;
            }

            //todo 异步加载资源

            view.Show(context);

            DealBgType(view, viewShowStyle.bgType);

            DealAnimationType(view, viewShowStyle.animationType);
        }

        public void OpenPriorView(bool refresh = false)
        {
            //弹栈
        }

        public void OpePriorView(IPopContext newContext)
        {
            //弹栈
        }


        protected void DealBgType(IPopView view, BackGroundType type)
        {
            
        }

        protected void DealAnimationType(IPopView view, AnimatinType aniamtion)
        {
            
        }
    }
}
