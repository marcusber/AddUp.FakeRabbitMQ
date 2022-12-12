﻿using System.Collections.Concurrent;
using RabbitMQ.Client;

namespace AddUp.RabbitMQ.Fakes
{
    public sealed class RabbitServer
    {
        public RabbitServer()
        {
            Exchanges = new ConcurrentDictionary<string, RabbitExchange>();
            // Pre-declare the default exchange.
            // https://www.rabbitmq.com/tutorials/amqp-concepts.html#exchange-default
            Exchanges[""] = new RabbitExchange(ExchangeType.Direct, this)
            {
                Name = "",
                Arguments = null,
                AutoDelete = false,
                IsDurable = true,
            };
            Queues = new ConcurrentDictionary<string, RabbitQueue>();
        }

        public ConcurrentDictionary<string, RabbitExchange> Exchanges { get; }
        public ConcurrentDictionary<string, RabbitQueue> Queues { get; }

        /// <summary>
        /// If true, deliveries to consumers will execute in a blocking manner, meaning the publish will not
        /// finish until the message has reached all registered consumers.
        /// </summary>
        public bool BlockingConsumerDelivery { get; set; }

        public void Reset()
        {
            Exchanges.Clear();
            Queues.Clear();
        }
    }
}