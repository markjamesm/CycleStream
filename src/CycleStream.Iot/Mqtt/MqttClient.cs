using System.Threading.Tasks;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
using Meadow;
using MQTTnet.Client.Options;

namespace CycleStream.Iot.Mqtt
{
    public static class MqttClient
    {
        public static async Task<bool> PublishMessage(string topic, string payload)
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    // Change the server IP in production
                    .WithTcpServer("192.168.0.131", 5004)
                    .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

                await mqttClient.DisconnectAsync();

                Resolver.Log.Info("MQTT application message is published.");
            }

            return true;
        }
    }
}