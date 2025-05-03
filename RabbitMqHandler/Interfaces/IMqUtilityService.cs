using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqHandler.Interfaces
{
    public interface IMqUtilityService
    {
        public Task PublishMessageToQueue(string routingKey, string eventData);
    }
}
