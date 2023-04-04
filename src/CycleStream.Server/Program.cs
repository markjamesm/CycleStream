namespace CycleStream.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MqttServer.Start_Server_With_WebSockets_Support();
        }
    }
}