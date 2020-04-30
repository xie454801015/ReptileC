using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeWorker
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
            // fanout 接收
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        channel.ExchangeDeclare(exchange: "logs", type: "fanout");

            //        var queueName = channel.QueueDeclare().QueueName;
            //        channel.QueueBind(queue: queueName,
            //                          exchange: "logs",
            //                          routingKey: "");

            //        Console.WriteLine(" [*] Waiting for logs.");

            //        var consumer = new EventingBasicConsumer(channel);
            //        channel.BasicConsume(queue: queueName,
            //                      autoAck: true,
            //                      consumer: consumer);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body.ToArray());
            //            Console.WriteLine(" [x] {0}", message);
            //        };


            //        Console.WriteLine(" Press [enter] to exit.");
            //        Console.ReadLine();
            //    }
            //}


            // direct 接收
            string severity = "3............";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");
                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queueName, "direct_logs", severity);
                    Console.WriteLine(" [*] Waiting for messages.");
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        var routingKey = ea.RoutingKey;
                        Console.WriteLine(" [x] Received '{0}':'{1}'",
                                          routingKey, message);
                    };
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }

        }
    }
}
