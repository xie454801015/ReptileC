using Nest;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var node = new Uri("http://localhost:9200/");
            //ConnectionSettings settings = new ConnectionSettings(node);
            //ElasticClient esclient = new ElasticClient(settings);

            //var existsResponse = esclient.IndexExists("inder_user");
            //var indexExists = existsResponse.Exists;
            //// 创建索引
            //var descriptIndex = new CreateIndexDescriptor("index_user")
            //    .Settings(s => s.NumberOfShards(5).NumberOfReplicas(0)
            //                    .Analysis(a => a.Normalizers(n => n.Custom("lowcase_normalizer", c => c.Filters("lowercase"))))//添加小写的过滤器
            //     );
            //var result = esclient.Index(descriptIndex);
            ////if (result != null && result.ApiCall != null)
            ////{ 

            ////}

            #region 简单的RabbitMq 测试
            //var factory = new ConnectionFactory();
            //factory.HostName = "localhost";
            //factory.UserName = "user";
            //factory.Password = "password";
            //using (var connecntion = factory.CreateConnection())
            //{
            //    using (var channel = connecntion.CreateModel())
            //    {
            //        channel.QueueDeclare("hello", false, false, false, null);// 创建一个名称为hello的消息队列
            //        string message = "Hello World";
            //        var body = Encoding.UTF8.GetBytes(message);
            //        channel.BasicPublish("", "hello", null, body); // 开始传递，
            //        Console.WriteLine("已发送：{0}", message);
            //        Console.ReadLine();

            //    }
            //}
            //// 模拟消息消费
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        channel.QueueDeclare("hello", false, false, false, null);

            //      var consumer = new EventingBasicConsumer(channel);
            //        channel.BasicConsume("hello", false, consumer);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var body = ea.Body.ToArray();
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine("已接收： {0}", message);
            //        };
            //        Console.ReadLine();
            //    }
            //}
            #endregion

            #region  工作队列 测试
            //var factory = new ConnectionFactory();
            //factory.HostName = "localhost";
            //factory.UserName = "user";
            //factory.Password = "password";

            //// 模拟发送器
            //// 多次发布
            //List<string> strMessage = new List<string>();
            //strMessage.Add("1............");
            //strMessage.Add("2........................");
            //strMessage.Add("3............");
            //strMessage.Add("4........");

            //foreach (var item in strMessage)
            //{
            //    using (var connection = factory.CreateConnection())
            //    {
            //        using (var channel = connection.CreateModel())
            //        {
            //            //队列发布
            //            channel.QueueDeclare("task_queue", true, false, false, null);
            //            string message = item;
            //            var properties = channel.CreateBasicProperties();
            //            properties.DeliveryMode = 2;

            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish("", "task_queue", properties, body);
            //            Console.WriteLine(" set {0}", message);
            //        }
            //    }
            //}
            #endregion
        }
    }

}
