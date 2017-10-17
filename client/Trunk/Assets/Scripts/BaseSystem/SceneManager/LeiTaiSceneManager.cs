using System;
using System.Collections.Generic;
using System.Collections;
using Monster.BaseSystem;
using PureMVC.Interfaces;
using UnityEngine;

namespace Monster.BaseSystem.SceneManager
{
    class LeiTaiSceneManager : BaseSceneManager
    {
        public new const string SCENE_NAME = "LeiTai";

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
            GameObject battleMainUI = ResourcesFacade.Instance.LoadPrefab("Prefab/UI/BattleMainUI/BattleMainUIPanel");
            UIManager.Intance.AddChild(battleMainUI.transform);
            Transform cameraTransform = GameObject.Find("PlayerCamera").transform;


            _proxyList = new List<IProxy>()
            {
                new SkillProxy(),
            };
            RegisterProxy(_proxyList);

            _mediatorList = new List<IMediator>() {
                   new BattleMainUIMediator(battleMainUI),
                   new WorldPlayerBattleMediator(cameraTransform),
            };
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
