using QPSLimit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LimitingServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var service = LimitingFactory.Build(LimitingType.LeakageBucket, 500, 200);
            var service = LimitingFactory.Build(LimitingType.TokenBucket, 5, 200);
            int count = 0;
            
            while (true)
            {
                var result = service.Request();
                //如果返回true，说明可以进行业务处理，否则需要继续等待
                if (result)
                {
                    //业务处理......
                    count++;
                    Console.WriteLine(count);
                }
                else
                {
                    Console.WriteLine("超限!");
                    count = 0;
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
