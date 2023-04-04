using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace CycleStream.Iot.Mqtt
{
    public sealed class MqttClient
    {
        public static async Task ConnectClient()
        {
            /*
             * This sample creates a simple managed MQTT client and connects to a public broker.
             *
             * The managed client extends the existing _MqttClient_. It adds the following features.
             * - Reconnecting when connection is lost.
             * - Storing pending messages in an internal queue so that an enqueue is possible while the client remains not connected.
             */

            var mqttFactory = new MqttFactory();

            using (var managedMqttClient = mqttFactory.CreateManagedMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("127.0.0.1")
                    .Build();

                var managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
                    .WithClientOptions(mqttClientOptions)
                    .Build();

                await managedMqttClient.StartAsync(managedMqttClientOptions);

                // The application message is not sent. It is stored in an internal queue and
                // will be sent when the client is connected.
                await managedMqttClient.EnqueueAsync("Topic", "Payload");

                Console.WriteLine("The managed MQTT client is connected.");

                // Wait until the queue is fully processed.
                SpinWait.SpinUntil(() => managedMqttClient.PendingApplicationMessagesCount == 0, 10000);

                Console.WriteLine($"Pending messages = {managedMqttClient.PendingApplicationMessagesCount}");
            }
        }
    }
}