using Meadow;
using Meadow.Devices;
using System;
using System.Threading.Tasks;

namespace CycleStream.Iot
{
    public class MeadowApp : App<F7FeatherV2>
    {
        EnvironmentalSensor _environmentalSensor;

        public override Task Run()
        {
            Console.WriteLine("Run...");

            return base.Run();
        }

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            var i2c = Device.CreateI2cBus();

            _environmentalSensor = new EnvironmentalSensor(i2c);
            _environmentalSensor.GatherData();

            return base.Initialize();
        }
    }
}