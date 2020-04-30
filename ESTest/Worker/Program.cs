using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "user";
            factory.Password = "password";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    // 告知 RabbitMQ，在未收到当前 Worker 的消息确认信号时，不再分发给消息，确保公平调度。
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    Console.WriteLine(" [*] Waiting for messages.");
                    var consumer = new EventingBasicConsumer(channel);
                    // 绑定消息接收事件。
                    channel.BasicConsume(queue: "task_queue",
                               autoAck: false,
                               consumer: consumer);
                    consumer.Received += (model, ea) =>
                      {
                          var body = ea.Body;
                          var message = Encoding.UTF8.GetString(body.ToArray());
                          Console.WriteLine("[x] Received{0}", message);
                          int dots = message.Split('.').Length - 1;
                          Thread.Sleep(dots * 1000);

                          Console.WriteLine("[x] Done");
                        // 手动发送消息确认信息
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                      };
                  
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
