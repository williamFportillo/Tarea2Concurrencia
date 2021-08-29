using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cinema.GateWay.BackgroundServices
{
    public class PaymentService : BackgroundService
    {
        private readonly IConnection _connection; //la conexión
        private readonly IModel _channel; // el canal
        private readonly EventingBasicConsumer _consumer; //consumidor

        public PaymentService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("receive-payment-queue", false, false, false, null);
            _consumer = new EventingBasicConsumer(_channel);
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Received += (model, content) =>
            {
                var body = content.Body.ToArray();
                var result = Encoding.UTF8.GetString(body);

            };

            _channel.BasicConsume("receive-payment-queue", true, _consumer);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
