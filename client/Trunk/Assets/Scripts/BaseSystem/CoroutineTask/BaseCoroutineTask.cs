namespace Monster.CoroutineTask
{
    public abstract class BaseCoroutineTask
    {
        /// <summary>
        /// 是否已完成
        /// </summary>
        public abstract bool isDone { get; }

        /// <summary>
        /// 进度
        /// </summary>
        public abstract float progress { get; }

        /// <summary>
        /// 描述
        /// </summary>
        public abstract string description { get; }
    }
}
