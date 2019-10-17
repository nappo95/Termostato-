using MQTTnet;
using MQTTnet.Client.Options;
using System;
using System.Threading;

namespace MQTTclient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Create a new MQTT client.
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                                .WithClientId("Client1")
                                .WithTcpServer("127.0.0.1")
                                .WithCredentials("bud", "%spencer%")
                                .WithTls()
                                .WithCleanSession()
                                .Build();

            await mqttClient.ConnectAsync(options, CancellationToken.None);
        }
    }
}
