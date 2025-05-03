using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMqHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqHandler.Services
{
    public class MqUtilityService : IMqUtilityService
    {
        private readonly MqSettings _settings;
        public MqUtilityService(IOptions<MqSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task PublishMessageToQueue(string routingKey, string eventData)
        {
            //var factory = new ConnectionFactory
            //{
            //    HostName = _settings.HostName,
            //    UserName = _settings.UserName,
            //    Password = _settings.Password
            //};
            //using (var connection = await factory.CreateConnectionAsync())
            //using (var channel = await connection.CreateChannelAsync())
            //{
            //    var message = Encoding.UTF8.GetBytes(eventData);
            //    await channel.BasicPublishAsync(exchange: "", routingKey: routingKey, body: message);
            //}

        }
    }
}
