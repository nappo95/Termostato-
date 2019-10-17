using System;
using MQTTbroker;
using MQTTnet;
using MQTTnet.Server;
using System.Threading.Tasks;
using MQTTnet.Protocol;
using MQTTnet.Client.Receiving;

namespace MQTTbroker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mqttServer = new MqttFactory().CreateMqttServer();
            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithConnectionValidator(c =>
                {
                    if (c.ClientId.Length < 10)
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                        return;
                    }

                    if (c.Username != "asd")
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                        return;
                    }

                    if (c.Password != "123Stella")
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                        return;
                    }

                    c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                });
            await mqttServer.StartAsync(optionsBuilder.Build());
            mqttServer.ApplicationMessageReceivedHandler = new MessageHandler();
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
            await mqttServer.PublishAsync("set-on");
            await mqttServer.PublishAsync("set-on");

            await mqttServer.StopAsync();
        }

       
    }

    public class MessageHandler : IMqttApplicationMessageReceivedHandler
    {
        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var tt = eventArgs.ApplicationMessage.Payload;

            string result = System.Text.Encoding.UTF8.GetString(tt);


            return Task.FromResult(0);
        }
    }
}
