using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace SiPintar.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqConsumerExtension : BackgroundService
    {
        private string? _queueName;
        private IModel? _consumerChannel;
        private IConnection? _connection;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                AutomaticRecoveryEnabled = true,
                VirtualHost = "SiPintarv5",
                DispatchConsumersAsync = true,
            };

            _connection = factory.CreateConnection();

            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "sipintar.pdam[1]", type: ExchangeType.Topic);
                _queueName = channel.QueueDeclare(queue: "WPF-1", durable: true, autoDelete: false, exclusive: false).QueueName;
                channel.QueueBind(queue: _queueName, exchange: "sipintar.pdam[1]", routingKey: "user.#");
            }

            _consumerChannel = _connection.CreateModel();
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
            consumer.Received += ConsumerOnReceivedAsync;
            _consumerChannel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ConsumerOnReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Debug.WriteLine($"Consumer1 - Received new message : {message}");
                _consumerChannel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Consumer1 - Error : {ex.Message}");
            }
        }
    }
}
