using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Monster.BaseSystem.UI
{
    public interface IPopView
    {
        GameObject view { get; }

        IPopContext context { get; }

        void BeforeEnter();
        void AfterEnter();
        void BeforeExit();
        void AfterExit();

        bool CheckCanShow(IPopContext context, ref string error);

        void Show(IPopContext context);
    }

    public interface IPopContext
    {
         
    }
}
