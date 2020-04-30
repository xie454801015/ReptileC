using Newtonsoft.Json;
using Reptile.Common;
using Reptile.Model;
using Reptile.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reptile
{
    /// <summary>
    /// 采集demo
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region 天气获取
            //CityWeatherReptile cityWeatherReptile = new CityWeatherReptile();
            //// 获取城市编码excel
            //string fielpath = cityWeatherReptile.GetCityExcel();
            //if (string.IsNullOrEmpty(fielpath))
            //{
            //    Console.WriteLine("未正确获取到城市编码文件!");
            //}
            //else
            //{
            //    // 读取excel 文件中的编码
            //    DataTable citys = ExcelHelper.ImportExcelFile(filePath: fielpath, true);
            //    DataRow[] cityRows = citys.Select("Country='北京'");
            //    if (cityRows == null || cityRows.Length == 0)
            //    {
            //        Console.WriteLine("没有需要检索的城市代码");
            //    }
            //    else
            //    {
            //        foreach (DataRow row in cityRows)
            //        {
            //            string citycode = row["CityCode"].ToString();
            //            try
            //            {
            //                bool result = cityWeatherReptile.GetWeatherResponse(citycode);
            //                if (result)
            //                    Console.WriteLine($"城市编码：{citycode}录入成功!");
            //                else
            //                    Console.WriteLine($"城市编码：{citycode} 暂时无法录入失败!");
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex);
            //                continue;
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 好物获取 测试版
            AwemeReptile awemeReptile = new AwemeReptile();
            awemeReptile.GetGood();

            #endregion
        }


        public static string GetWebClient(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();

            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }

    }
}
