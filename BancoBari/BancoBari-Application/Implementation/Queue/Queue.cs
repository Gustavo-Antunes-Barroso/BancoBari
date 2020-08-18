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
            var host = _configuration.GetSection("QueueHost").Value;
            var factory = new ConnectionFactory() { HostName = host, RequestedHeartbeat = TimeSpan.FromMinutes(1) };
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
                            MontarObjeto()
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

        public QueueObject MontarObjeto()
        {
            var lstMensagem = (List<MensagemDto>)_mensagensService.SelecionarTodos().Result.Object;
            var mensagem = lstMensagem.FirstOrDefault();

            //Buscar sistema no banco
            var response = new QueueObject
            {
                MensagemDescricao = mensagem.Descricao,
                //setado como NewGuid para teste e popular o banco
                //MensagemId = mensagem.Id,
                MensagemId = Guid.NewGuid(),
                //HardCode
                NomeSitema = "Publisher",
                SistemaId = Guid.Parse("07ccd9ab-c9ee-437a-a992-291417f1f23e")
            };

            return response;
        }
    }
}
