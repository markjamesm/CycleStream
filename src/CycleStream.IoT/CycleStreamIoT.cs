using Meadow;
using Meadow.Devices;
using System;
using System.Threading.Tasks;

namespace CycleStream.Iot
{
    public class CycleStreamIoT : App<F7FeatherV2>
    {
        public override Task Run()
        {
            Console.WriteLine("Run...");

            return base.Run();
        }

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            var i2c = Device.CreateI2cBus();

            var environmentalSensor = new EnvironmentalSensor(i2c);
            environmentalSensor.PollSensor();

            return base.Initialize();
        }
    }
}