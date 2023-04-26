using System.Threading.Tasks;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
using Meadow;
using MQTTnet.Client.Options;

namespace CycleStream.Iot.Mqtt
{
    public class MqttClient
    {
        private readonly IMqttClient _mqttClient;

        public MqttClient(MqttFactory mqttFactory)
        {
            _mqttClient = mqttFactory.CreateMqttClient();
        }

        public async Task Start()
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
            // Change the server IP in production
            .WithTcpServer("192.168.0.131", 61616)
            .WithCredentials(Secrets.ActiveMqUsername, Secrets.ActiveMqPassword)
            .Build();

            await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
        }

        public async Task PublishMessage(string topic, string payload)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

            Resolver.Log.Info("MQTT application message is published.");
        }
    }
}