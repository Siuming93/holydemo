using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotFix.GameFrame.Common
{
    public class TickProxy
    {
        private static TickProxy m_Instance = new TickProxy();
        public static TickProxy Instance
        {
            get { return m_Instance; }
        }

        private event Action m_TickEvent;
        public event Action TickEvent
        {
            add { m_TickEvent += value; }
            remove { m_TickEvent -= value; }
        }

        public void Init()
        {
            m_ItorList = new LinkedList<IEnumerator>();
        }
        public void Dispose()
        {
            m_TickEvent = null;
            StopAllCoroutine();
        }

        public void Tick()
        {
            if (m_TickEvent != null)
                m_TickEvent.Invoke();

            var node = m_ItorList.First;
            while (node != null)
            {
                if (!node.Value.MoveNext())
                {
                    m_ItorList.Remove(node);
                }
                node = node.Next;
            }
        }

        private LinkedList<IEnumerator> m_ItorList; 

        public void StartCoroutine(IEnumerator itor)
        {
            m_ItorList.AddLast(itor);
        }

        public void StopCoroutine(IEnumerator itor)
        {
            if (m_ItorList != null)
            {
                var node = m_ItorList.First;
                while (node != null)
                {
                    if (node.Value == itor)
                    {
                        m_ItorList.Remove(node);
                    }
                }
            }
        }

        public void StopAllCoroutine()
        {
            m_ItorList.Clear();
        }
    }
}
