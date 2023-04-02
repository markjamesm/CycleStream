using Meadow;
using Meadow.Devices;
using CycleStream.Iot.Sensors;
using System;
using System.Threading.Tasks;

namespace CycleStream.Iot
{
    public class CycleStreamIot : App<F7FeatherV2>
    {
        public override Task Run()
        {
            Console.WriteLine("Run...");

            return base.Run();
        }

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            var environmentalSensor = new EnvironmentalSensor();
            environmentalSensor.Poll();

            //var displayController = new DisplayController();
            //displayController.Display();

            return base.Initialize();
        }
    }
}