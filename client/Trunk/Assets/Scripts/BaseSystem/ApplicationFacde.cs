using PureMVC.Patterns.Facade;
using PureMVC.Patterns.Mediator;
using PureMVC.Patterns.Observer;

namespace Monster.BaseSystem
{
    public class ApplicationFacade : Facade
    {
        private static ApplicationFacade mInstance;
        public static ApplicationFacade Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new ApplicationFacade();
                return mInstance;
            }
        }

        private ApplicationFacade()
        {

        }

        public void RegisterNotificaiton(string notificaitonName, Mediator mediator)
        {
            view.RegisterObserver(notificaitonName, new Observer(mediator.HandleNotification, mediator));
        }

        public void ReMoveNotificatiion(string notificationName, Mediator mediator)
        {
            view.RemoveObserver(notificationName, mediator);
        }
    }
}
