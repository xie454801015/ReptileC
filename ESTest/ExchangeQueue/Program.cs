using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
            };
            List<string> strMessage = new List<string>();
            strMessage.Add("1............");
            strMessage.Add("2........................");
            strMessage.Add("3............");
            strMessage.Add("4........");

            // 简单广播 fanout 
            //foreach (var item in strMessage)
            //{
            //    using (var connection = factory.CreateConnection())
            //    {
            //        using (var channel = connection.CreateModel())
            //        {
            //            channel.ExchangeDeclare(exchange: "logs", type: "fanout");
            //            var message = item;
            //            var body = Encoding.UTF8.GetBytes(message);
            //            channel.BasicPublish(exchange: "logs",
            //                                 routingKey: "",
            //                                 basicProperties: null,
            //                                 body: body);
            //            Console.WriteLine(" [x] Sent {0}", message);
            //        }
            //        Console.WriteLine(" Press [enter] to exit.");
            //        Console.ReadLine();
            //    }
            //}

            // 存在routingkey
            foreach (var item in strMessage)
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");
                        var severity = (item.Length > 0) ? item: "info";
                        var message = item;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "direct_logs",
                                             routingKey: severity,
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine(" [x] Sent {0}:{1}",severity ,message);
                    }
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
