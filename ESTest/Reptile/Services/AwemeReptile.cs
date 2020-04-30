using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reptile.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reptile.Services
{
    public class AwemeReptile
    {
        private string _host = "https://aweme.snssdk.com";
        private string _baseUrl = "/aweme/v1/promotion/user/promotion/list/";

        //private string _versionCode = "6.8.0";
        //private string  _passrgion="1";
        //private string _passroute = "1";
        //private string _js_sdk_version = "1.17.4.3";
        //private string _app_name = "aweme";

        //private string _vid = "ABA16B81-AE39-4B64-84EA-13D1D74BA915";
        //private string _app_viersion = "6.8.0";
        //private string _device_id = "3528998095";
        //private string _channel = "App Store";
        //private string _mcc_mcc = "46002";
        /// <summary>
        ///  必要参数 
        /// </summary>
        private string _aid = "1128";
        //private string _screen_width = "750";
        //private string _openudid = "604d000c1046107e3cb271cbe6b4de7f40f3b289";
        //private string _os_api = "18";
        //private string _ac = "WIFI";
        //private string _os_version = "12.3.1";
        //private string _device_platform = "iphone";
        //private string _build_number = "68013";
        //private string _device_type = "iPhone8,1";
        //private string _iid = "76710993012";

        //private string _idfa = "807EAEC6-C41E-4458-926D-7F04320FC6A2";
        //private string _cursor = "0";
        
        /// <summary>
        /// 必要参数
        /// </summary>
        private string _user_id = "67579558320";
        /// <summary>
        ///  反馈的商品数量
        /// </summary>
        private string _count = "10";
        //private string _sec_user_id = "MS4wLjABAAAA1i5_lgmBJJM1fsNjEt2oiz1ZxqaSi21tcELCcelqOs4";


        public string GetUrl(string userid="67579558320",int count=10)
        {
            string resutUrl = _host + _baseUrl + $"?" 
                //$"versiong_code={_versionCode}&pass-region={_passrgion}&pass-route={_passroute}&js_sdk_version={_js_sdk_version}&app_name={_app_name}&vid={_vid}&app_version={_app_viersion}&" 
                //+$"device_id={_device_id}" 
                //+$"&channel={ System.Web.HttpUtility.UrlEncode(_channel)}" 
                //+$"&mcc_mnc={_mcc_mcc}" 
                +$"&aid={_aid}" 
                //$"&screen_width={_screen_width}" 
                // +$"&openudid={_openudid}" 
                //+$"&os_api={_os_api}&ac={_ac}&os_version={_os_version}" 
                //+$"&device_platform={_device_platform}&" 
                //+$"build_number={_build_number}"
                //+ $"&device_type={_device_type}"
                //+$"&iid={_iid}" 
                + $"&user_id={userid}" 
                +$"&count={count}"; 
                //+$"&sec_user_id={_sec_user_id}";
            return resutUrl;
        }

        public bool GetGood()
        {
            string url = GetUrl();
            string result = HttpHelper.SendGet(url);
            JObject jsonObject = null;
            try
            {
                jsonObject = JObject.Parse(result);
                if (jsonObject.Count == 1 || (int)(jsonObject["status_code"]) != 0)
                {
                    return false;
                }
                else
                {
                    string promotions = jsonObject["promotions"].ToString();
                    JArray jlist = JArray.Parse(promotions);
                    List<TestModel> testModels = new List<TestModel>();
                    foreach (var item in jlist)
                    {
                        testModels.Add(JsonConvert.DeserializeObject<TestModel>(item.ToString()));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

    }
    public class TestModel
    { 
        public int Price { get; set; }
        public string Goods_source { get; set; }
        public string title { get; set; }
    }

}
