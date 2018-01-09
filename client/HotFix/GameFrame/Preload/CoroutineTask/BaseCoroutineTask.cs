using System;
using System.Collections;
using System.Threading;

namespace HotFix.GameFrame.PreLoad
{
    public abstract class BaseCoroutineTask : ICoroutineResult
    {
        /// <summary>
        /// 进度
        /// </summary>
        public virtual float Progress { get; protected set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; protected set; }

        public abstract IEnumerator Run();
        public abstract void Dispose();
        public virtual bool IsCompleted { get; protected set; }
        public virtual object AsyncState { get; protected set; }
    }
}
