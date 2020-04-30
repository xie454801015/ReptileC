using Newtonsoft.Json;
using Reptile.Common;
using Reptile.Model;
using Reptile.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reptile.Services
{
    public   class CityWeatherReptile
    {
        private string  _host = "http://api.help.bj.cn";
        private string _baseUrl = "/apis/weather/";

        public bool GetWeatherResponse(string  cityId,  int reconnecntTimes=10)
        {
            string url = _host + _baseUrl + $"?id={cityId}";
            CityWeather data = new CityWeather();
            if (reconnecntTimes <= 0)
            {
                reconnecntTimes = 10;
            }
            for(int i=1;i<=reconnecntTimes;i++)
            {
                Console.WriteLine($"尝试对城市编码:{cityId} 第{i}次抓取");
                string result = HttpHelper.SendGet(url);
                data = JsonConvert.DeserializeObject<CityWeather>(result);
                if (data.Status.Equals("0"))
                {
                    if (data.CityCode.Equals(cityId))
                    {
                        data.CreateTime = DateTime.Now;
                        CityWeatherRepository cityWehterRepository = new CityWeatherRepository();
                        cityWehterRepository.InsertWeather(data);
                        return true;
                    }
                    else
                        Console.WriteLine("城市id不匹配，尝试再次请求!");
                        continue;
                }
                else
                {
                    Console.WriteLine($"城市{cityId}:{data.Msg}");
                    return false;  
                }
            }
            return false;
        }
        public string GetCityExcel()
        {
            string url = "http://api.help.bj.cn/api/CityCode.XLS";
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "Get";
            request.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 64.0.3282.140 Safari / 537.36 Edge / 18.17763";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return "";
            }
            Stream fileStream = response.GetResponseStream();
            string name = url.Substring(url.LastIndexOf('/') + 1);
            string workpath = Path.Combine( System.IO.Directory.GetCurrentDirectory(),"upload");
            string filePath = Path.Combine(workpath, name);
            if (!Directory.Exists(workpath))
               Directory.CreateDirectory(workpath);
            //创建本地文件写入流
            Stream fs = new FileStream(filePath,FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = fileStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                fs.Write(bArr, 0, size);
                size = fileStream.Read(bArr, 0, (int)bArr.Length);
            }
            fs.Close();
            fileStream.Close();
            return filePath;
        }

    }
}
