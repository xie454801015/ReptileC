using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QPSLimit
{
    public class LeakageBucketLimitingService:ILimitingService
    {
        private LimitedQueue<object> limitedQueue = null;
        private CancellationTokenSource cancelToken;
        private Task task = null;
        /// <summary>
        /// 最大吞吐量
        /// </summary>
        private int maxTPS;
        /// <summary>
        /// 桶容量
        /// </summary>
        private int limitSize;
        private object lckObj = new object();

        public LeakageBucketLimitingService(int maxTPS, int limitSize)
        {
            this.limitSize = limitSize;
            this.maxTPS = maxTPS;

            if (this.limitSize <= 0)
                this.limitSize = 100;
            if (this.maxTPS <= 0)
                this.maxTPS = 1;

            limitedQueue = new LimitedQueue<object>(limitSize);
            cancelToken = new CancellationTokenSource();
            task = Task.Factory.StartNew(new Action(TokenProcess), cancelToken.Token);
        }


        /// <summary>
        ///  每间隔 sleep 毫秒排放一个队列空间
        /// </summary>
        private void TokenProcess()
        {
            int sleep = 1000 / maxTPS;
            if (sleep == 0)
                sleep = 1;

            DateTime start = DateTime.Now;
            while (cancelToken.Token.IsCancellationRequested == false)
            {
                try
                {

                    if (limitedQueue.Count > 0)
                    {
                        lock (lckObj)
                        {
                            if (limitedQueue.Count > 0)
                                limitedQueue.Dequeue();
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    // 如果运行时间过短，则等待一部分时间再次排放流量
                    if (DateTime.Now - start < TimeSpan.FromMilliseconds(sleep))
                    {
                        int newSleep = sleep - (int)(DateTime.Now - start).TotalMilliseconds;
                        if (newSleep > 1)
                            Thread.Sleep(newSleep - 1); //做一下时间上的补偿
                    }
                    start = DateTime.Now;
                }
            }
        }

        public void Dispose()
        {
            cancelToken.Cancel();
        }

        public bool Request()
        {
            // 桶内数量过多 则限制注入
            if (limitedQueue.Count >= limitSize)
                return false;
            // 桶内可以继续注水
            lock (lckObj)
            {
                // 桶内数量过多 则限制注入
                if (limitedQueue.Count >= limitSize)
                    return false;
                // 否则注入一个队列
                return limitedQueue.Enqueue(new object());
            }
        }

    }
}
