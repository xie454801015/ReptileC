using Reptile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reptile.Repository
{
    public class CityWeatherRepository
    {
      

        public int InsertWeather(CityWeather cityWeather)
        {
            string sql = "insert into CityWeather(City,CityCode,Temp,Today,CreateTime) values(@City,@CityCode,@Temp,@Today,@CreateTime)";

            return DapperHelper<CityWeather>.Execute(sql, cityWeather);
        }
    }
}
