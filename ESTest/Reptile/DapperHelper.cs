using Dapper;
using Reptile.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reptile
{
    public  class DapperHelper<T>
    {
        private static string _connectionString = "Server=127.0.0.1;Database=WeatherTest;User Id=sa;Password=123456;";

        public static List<T> Query(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<T>(sql, param).ToList();
            }
        }
        /// <summary>
        /// 查询第一个数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirst(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.QueryFirst<T>(sql, param);
            }
        }

        /// <summary>
        /// 查询第一个数据没有返回默认值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirstOrDefault(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<T>(sql, param);
            }
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingle(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.QuerySingle<T>(sql, param);
            }
        }

        /// <summary>
        /// 查询单条数据没有返回默认值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingleOrDefault(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.QuerySingleOrDefault<T>(sql, param);
            }
        }

        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Execute(sql, param);
            }
        }

        /// <summary>
        /// Reader获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.ExecuteReader(sql, param);
            }
        }

        /// <summary>
        /// Scalar获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.ExecuteScalar(sql, param);
            }
        }

        /// <summary>
        /// Scalar获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteScalarForT(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.ExecuteScalar<T>(sql, param);
            }
        }

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> ExecutePro(string proc, object param)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                List<T> list = con.Query<T>(proc,
                    param,
                    null,
                    true,
                    null,
                    CommandType.StoredProcedure).ToList();
                return list;
            }
        }


        /// <summary>
        /// 事务1 - 全SQL
        /// </summary>
        /// <param name="sqlarr">多条SQL</param>
        /// <param name="param">param</param>
        /// <returns></returns>
        public static int ExecuteTransaction(string[] sqlarr)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (var sql in sqlarr)
                        {
                            result += con.Execute(sql, null, transaction);
                        }

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 事务2 - 声明参数
        ///demo:
        ///dic.Add("Insert into Users values (@UserName, @Email, @Address)",
        ///        new { UserName = "jack", Email = "380234234@qq.com", Address = "上海" });
        /// </summary>
        /// <param name="Key">多条SQL</param>
        /// <param name="Value">param</param>
        /// <returns></returns>
        public static int ExecuteTransaction(Dictionary<string, object> dic)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (var sql in dic)
                        {
                            result += con.Execute(sql.Key, sql.Value, transaction);
                        }

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}

