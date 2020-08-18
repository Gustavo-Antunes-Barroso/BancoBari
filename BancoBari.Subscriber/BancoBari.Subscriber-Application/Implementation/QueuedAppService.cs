using BancoBari.Subscriber_Application.Interfaces;
using BancoBari.Subscriber_Domain.Entities;
using BancoBari.Subscriber_Domain.Intefaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace BancoBari.Subscriber_Application.Implementation
{
    public class QueuedAppService : IQueuedAppService
    {
        private readonly IConfiguration _configuration;
        private readonly IQueuedRepository _queuedRepository;
        public QueuedAppService(IConfiguration configuration, 
            IQueuedRepository queuedRepository)
        {
            _configuration = configuration;
            _queuedRepository = queuedRepository;
        }
        public void BuscarNaFila()
        {
            var host = _configuration.GetSection("QueueHost").Value;
            var factory = new ConnectionFactory() { HostName = host };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    while (channel.IsOpen)
                    {
                        Thread.Sleep(1);
                        channel.QueueDeclare(queue: "Mensagem",
                          durable: false,
                          exclusive: false,
                          autoDelete: false,
                          arguments: null
                          );


                        var consumer = new EventingBasicConsumer(channel);
                        QueuedObject obj = new QueuedObject();
                        string message = "";
                        string inseriu = null;
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            message = Encoding.UTF8.GetString(body);
                            obj = JsonConvert.DeserializeObject<QueuedObject>(message);
                            inseriu = _queuedRepository.Inserir(obj).Result.ToString();
                        };

                        channel.BasicConsume(queue: "Mensagem",
                            autoAck: true,
                            consumer: consumer
                            );

                        if (!string.IsNullOrWhiteSpace(inseriu))
                            channel.Close();
                    }
                   

                }
                connection.Close();
            }
        }
    }
}
