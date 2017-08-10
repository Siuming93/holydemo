using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns.Facade;
using PureMVC.Interfaces;

namespace Monster.BaseSystem
{
    public static class ApplicationFacade
    {
        public static IFacade  Instance;
        static ApplicationFacade()
        {
            Instance = Facade.GetInstance(GetFacade);
        }

        private static IFacade GetFacade()
        {
            return new Facade();
        }
    }
}
