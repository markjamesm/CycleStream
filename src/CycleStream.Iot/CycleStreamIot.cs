using Meadow;
using Meadow.Devices;
using CycleStream.Iot.Sensors;
using System;
using System.Threading.Tasks;

namespace CycleStream.Iot
{
    public class CycleStreamIot : App<F7FeatherV2>
    {
        DisplayController _displayController;

        public override Task Run()
        {
            Console.WriteLine("Run...");

            if (_displayController != null)
            {
                _displayController.Update();
            }

            return base.Run();
        }

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            var environmentalSensor = new EnvironmentalSensor();
            environmentalSensor.Poll();

            var projLab = ProjectLab.Create();

            if (projLab.Display is { } display)
            {
                Resolver.Log.Trace("Creating DisplayController");
                _displayController = new DisplayController(display);
                Resolver.Log.Trace("DisplayController up");
            }

            //var displayController = new DisplayController();
            //displayController.Display();

            return base.Initialize();
        }
    }
}