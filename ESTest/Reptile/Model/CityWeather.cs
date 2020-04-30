using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reptile.Model
{
    public class CityWeather
    {

        /// <summary>
        /// 代码反馈状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 反馈信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// id记录 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 城市英文名
        /// </summary>
        public string Cityen { get; set; }
        /// <summary>
        /// 城市编码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 实时温度
        /// </summary>
        public decimal Temp { get; set; }
        /// <summary>
        /// 华氏温度
        /// </summary>
        public decimal Tempf { get; set; }
        /// <summary>
        /// 风向
        /// </summary>
        public string Wd { get; set; }
        /// <summary>
        /// 风向英文
        /// </summary>
        public string W { get; set; }
        /// <summary>
        /// 风力
        /// </summary>
        public string WdForce { get; set; }
        /// <summary>
        /// 风速
        /// </summary>
        public string WdSpd { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpTime { get; set; }
        /// <summary>
        /// 天气
        /// </summary>
        public string Weather { get; set; }
        /// <summary>
        /// 天气状况英文
        /// </summary>
        public string Weatheren { get; set; }
        /// <summary>
        /// 天气状况图标
        /// </summary>
        public string WeatherImg { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        public string Stp { get; set; }
        /// <summary>
        /// 能见度
        /// </summary>
        public string Wisib { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        public string Humidity { get; set; }
        /// <summary>
        /// 降雨量
        /// </summary>
        public string Prcp { get; set; }
        /// <summary>
        /// 24小时降雨量
        /// </summary>
        public string Prcp24h { get; set; }
        /// <summary>
        /// aqi
        /// </summary>
        public string Aqi { get; set; }
        /// <summary>
        /// pm2.5
        /// </summary>
        public string Pm25 { get; set; }
        /// <summary>
        /// 今天日期
        /// </summary>
        public string Today { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }


    //    {
    //    "status": "0",       //反馈代码 0成功
    //    "msg": "反馈信息",      //反馈信息
    //    "cityen": "changchun",       //城市名称英文
    //    "city": "长春",       //城市名称
    //    "citycode": "101060101",       //城市编码
    //    "temp": "10",       //实时温度
    //    "tempf": "50",       //华氏温度
    //    "wd": "西风",       //风向
    //    "wden": "W",       //风向英文
    //    "wdforce": "3级",       //风力
    //    "wdspd": "<12km/h",       //风速
    //    "uptime": "12:00",       //更新时间
    //    "weather": "晴",       //天气状况
    //    "weatheren": "Sunny",       //天气状况英文
    //    "weatherimg": "d00",       //天气状况图标
    //    "stp": "994",       //气压
    //    "wisib": "35000",       //能见度
    //    "humidity": "46%",       //湿度
    //    "prcp": "0",       //降雨
    //    "prcp24h": "2.2",       //24小时降雨量
    //    "aqi": "22",       //AQI
    //    "pm25": "20",       //PM2.5
    //    "today": "10月17日(星期一)"      //今天日期
    //}
}
