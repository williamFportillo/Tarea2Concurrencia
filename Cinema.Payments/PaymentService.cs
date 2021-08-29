using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Cinema.Payments
{
    public class PaymentService : BackgroundService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IConnection _connection; //la conexión
        private readonly IModel _channel; // el canal
        private readonly EventingBasicConsumer _consumer; //consumidor
        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("payment-queue", false, false, false, null);
            _consumer = new EventingBasicConsumer(_channel);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Received += async (model, content) =>
            {
                var body = content.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var paymentInformation = JsonConvert.DeserializeObject<PaymentInformation>(json);
                var paymentResult = await ProcessPayment(paymentInformation, cancellationToken);
                var message = $"El pago para {paymentInformation.BasketId} resultó {paymentResult}";
                Console.WriteLine(message);
                NotifyPaymentService(message);
            };

            _channel.BasicConsume("payment-queue", true, _consumer);
            return Task.CompletedTask;
        }

        private async Task<bool> ProcessPayment(PaymentInformation paymentInformation, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);

            return true;
        }

        private void NotifyPaymentService(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("receive-payment-queue", false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(string.Empty, "receive-payment-queue", null, body);
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
