using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QPSLimit
{
    public static class TokenBucketLimitManager
    {
        private static ConcurrentDictionary<String, ILimitingService> map = new ConcurrentDictionary<string, ILimitingService>();

        public static ILimitingService GetLeakageBucketLimitingService(String key, int maxQPS, int limitSize)
        {
            return map.GetOrAdd(key, m => { return new LeakageBucketLimitingService(maxQPS, limitSize); });
            //return map.GetOrAdd(key, m => { return new TokenBucketLimitingService(maxQPS, limitSize); });
        }
    }
}
