using System;
using System.Collections.Generic;
using System.Collections;
using Monster.BaseSystem;
using PureMVC.Interfaces;

namespace Monster.BaseSystem.SceneManager
{
    class CitySceneManager : BaseSceneManager
    {
        private List<IMediator> _mediatorList;
        private List<IProxy> _proxyList;
        public override IEnumerator AfterLeaveScene(object data)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator BeforeEnterScene(object data)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator BeforeLeaveScene(object data)
        {
            RemoveProxy(_proxyList);
            RemoveMediator(_mediatorList);
            yield return 0;
        }

        public override IEnumerator OnEnterScene(object data)
        {
            _proxyList = new List<IProxy>()
            {
            };
            _mediatorList = new List<IMediator>() {
            };

            RegisterProxy(_proxyList);
            RegisterMediator(_mediatorList);
            yield return 0;
        }

        private void RegisterProxy(List<IProxy> list)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                ApplicationFacade.Instance.RegisterProxy(list[i]);
            }
        }

        private void RemoveProxy(List<IProxy> list)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                ApplicationFacade.Instance.RemoveProxy(list[i].ProxyName);
            }
        }

        private void RegisterMediator(List<IMediator> list)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                ApplicationFacade.Instance.RegisterMediator(list[i]);
            }
        }

        private void RemoveMediator(List<IMediator> list)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                ApplicationFacade.Instance.RemoveMediator(list[i].MediatorName);
            }
        }
    }
}
