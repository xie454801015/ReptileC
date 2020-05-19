using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QPSTest1
{
    /// <summary>
    ///  
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(10000);
            //List<string> str1 = new List<string>();
            //Thread t1 = new Thread(GetTest1);
            //Thread t2 = new Thread(GetTest2);
            //Thread t3 = new Thread(GetTest3);
            ////t1.Start();
            //t2.Start();
            ////t3.Start();
            //Thread.Sleep(50000);
            for (int i = 0; i < 7; i++)
            {
                GetTest2();
            }
            Console.ReadLine();
            
        }

        public  static void   GetTest1()
        {
     
            for (int i = 0; i < 2000; i++)
            {
                string str = GetData("https://localhost:44347/home/test1");
                Console.WriteLine($"Test1:{str}:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}-------{i}");
            }
            Console.WriteLine("结束1");
        }
        public static void GetTest2()
        {
            int success = 0;
            int count = 0;
            DateTime start = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
                string str = GetData("https://localhost:44347/home/test2");
                //Console.WriteLine($"Test2:{str}:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}-----{i}");
                if (str.Equals("22222"))
                {
                    success++;
                }
                count++;
            }
            int newSleep =(int)(DateTime.Now - start).TotalMilliseconds;
            Console.WriteLine($"结束2:总数{count}---成功{success}---耗时{newSleep}ms，成功率={success*100/count}%，qps估算={success*1000/newSleep}");
        }
        public static void GetTest3()
        {

            for (int i = 0; i < 2000; i++)
            {
                string str = GetData("https://localhost:44347/home/test1");
                Console.WriteLine($"Test3:-3:{str}:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff")}----------{i}");
            }
            Console.WriteLine("结束3");
        }

        public static string GetData(string  url)
        {
            using (var client = new WebClient())
            {
                //client.Timeout = timeOut;
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36";
                client.Encoding = Encoding.UTF8;
                string result = client.DownloadString(url);
                return result;
            }

        }


    }
}
