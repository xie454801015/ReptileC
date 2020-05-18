using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPSLimit
{
    /// <summary>
    /// 限流队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LimitedQueue<T> : Queue<T>
    {
        private int limit = 0;
        public const string QueueFulled = "TTP-StreamLimiting-1001";

        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public LimitedQueue()
             : this(0)
        { }

        public LimitedQueue(int limit)
             : base(limit)
        {
            this.Limit = limit;
        }

        public new bool Enqueue(T item)
        {
            if (limit > 0 && this.Count >= this.Limit)
            {
                return false;
            }
            base.Enqueue(item);
            return true;
        }
    }
    /// <summary>
    /// 限流模式
    /// </summary>
    public enum LimitingType
    {
        TokenBucket,//令牌桶模式
        LeakageBucket//漏桶模式
    }


}
