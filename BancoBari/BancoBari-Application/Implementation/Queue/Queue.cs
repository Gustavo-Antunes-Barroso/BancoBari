using BancoBari_Application.Interfaces.Mensagem;
using BancoBari_Application.Interfaces.Queue;
using BancoBari_Domain.Dto.Mensagem;
using BancoBari_Domain.Dto.Queue;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BancoBari_Application.Implementation.Queue
{
    public class Queue : IQueue
    {
        private readonly IConfiguration _configuration;
        private readonly IMensagensAppServices _mensagensService;
        public Queue(IConfiguration configuration, IMensagensAppServices mensagensService)
        {
            _configuration = configuration;
            _mensagensService = mensagensService;
        }
        public void Enfileirar()
        {
            var obj = MontarObjeto();
            if (obj != null)
            {
                var host = _configuration.GetSection("QueueHost").Value;
                var factory = new ConnectionFactory() { HostName = host, RequestedHeartbeat = TimeSpan.FromMinutes(1), Port = AmqpTcpEndpoint.UseDefaultPort };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "Mensagem",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        var body = Encoding.UTF8.GetBytes(
                            JsonConvert.SerializeObject(
                                obj
                                ));

                        channel.BasicPublish(exchange: "",
                            routingKey: "Mensagem",
                            basicProperties: null,
                            body: body
                            );
                    }
                    connection.Close();
                }
            }
        }

        public QueueObject MontarObjeto()
        {
            var lstMensagem = (List<MensagemDto>)_mensagensService.SelecionarTodosNaoIntegrados().Result.Object;
            var mensagem = lstMensagem?.FirstOrDefault();
            if (mensagem != null)
            {
                var response = new QueueObject
                {
                    MensagemDescricao = mensagem.Descricao,
                    MensagemId = mensagem.Id,
                    SistemaId = mensagem.SistemaId
                };

                return response;
            }
            return null;
        }
    }
}
