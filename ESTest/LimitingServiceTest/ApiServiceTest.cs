using QPSLimit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LimitingServiceTest
{
    public class ApiServiceTest
    {
        private static string jingdongApiUrl = "http://commonapi.xiguaji.com/Myq/GetJdUnionItems";

        public string GetRequset(List<string> skuids,int timeOut = 10000)
        {
            var skuidsStr = String.Join(",", skuids.ToArray());
            string url = jingdongApiUrl + $"?skuIds={skuidsStr}";

            using (var client = new WebClient())
            {
                //client.Timeout = timeOut;
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36";
                client.Encoding = Encoding.UTF8;
                string result = client.DownloadString(url);
                return result;
            }
        }

        public  string QPSLimiting()
        {
            var service = LimitingFactory.Build(LimitingType.TokenBucket, 5, 200);
            int count = 0;
            var skuids = GetSkuIds();
            while (true)
            {
                var result = service.Request();
                //如果返回true，说明可以进行业务处理，否则需要继续等待
                if (result)
                {
                    GetRequset(skuids);
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
        }

        private List<string> GetSkuIds()
        {
            return new List<string>()
            {
                "303383081592",
                "30338308159",
                "100004652001",
                "58903645957",
                "7143389",
                "3196532",
                "100012014970"
            };
        }

    }


}
