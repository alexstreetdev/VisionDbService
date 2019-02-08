using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VisionDatabase.DbModels;
using VisionDbService.MessageHandlers;
using VisionModels.Messages;

namespace VisionDbService
{
    public class MessageClient
    {
        public const string MQ_EXCHANGE_NAME = "visionExchange";
        public const string MQ_QUEUE_NAME = "visionDbQueue";

        private readonly IConnection _mQconn;
        private readonly IModel _channel;
        private readonly ServiceProvider _serviceProvider;

        public MessageClient(ServiceProvider sp)
        {
            _serviceProvider = sp;

            var factory = new ConnectionFactory()
            {
                UserName = "vision",
                Password = "vision",
                HostName = "192.168.0.50"
            };
            _mQconn = factory.CreateConnection();

            _channel = _mQconn.CreateModel();

            _channel.ExchangeDeclare(MQ_EXCHANGE_NAME, ExchangeType.Topic);
            _channel.QueueDeclare(MQ_QUEUE_NAME, false, false, false, null);
            _channel.QueueBind(MQ_QUEUE_NAME, MQ_EXCHANGE_NAME, "vision.*.*", null);

        }


        public void Run()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += ConsumerOnReceived;
            String consumerTag = _channel.BasicConsume(MQ_QUEUE_NAME, false, consumer);
        }

        public void Stop()
        {
            _channel.Close();
            _mQconn.Close();
        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs bdea)
        {
            var p = bdea.BasicProperties.Type.ToLower();
            var body = bdea.Body;
            var bodystring = Encoding.UTF8.GetString(body);

            switch (p)
            {
                case "movement":
                    HandleMovement(bodystring);
                    break;
                case "facedetect":
                    HandleFaceDetected(bodystring);
                    break;
            }

            Console.WriteLine(bodystring);
            var routingKey = bdea.RoutingKey;
            Console.WriteLine(routingKey);
            _channel.BasicAck(bdea.DeliveryTag, false);
        }


        public void HandleMovement(string bodyString)
        {
            var m = JsonConvert.DeserializeObject<Movement>(bodyString);
            var handler = _serviceProvider.GetService<IMessageHandler<Movement>>();
            handler.Handle(m);
        }

        public void HandleFaceDetected(string bodyString)
        {
            var m = JsonConvert.DeserializeObject<FaceDetect>(bodyString);
            var handler = _serviceProvider.GetService<IMessageHandler<FaceDetect>>();
            handler.Handle(m);
        }

    }
}
