using Meadow;
using Meadow.Devices;
using System.Threading.Tasks;
using CycleStream.Iot.Sensors;

namespace CycleStream.Iot
{
    public class CycleStreamIot : App<F7FeatherV2>
    {
        private DisplayController _displayController;
        private IProjectLabHardware _projLab;
        private EnvironmentalSensor _environmentalSensor;

        public override Task Initialize()
        {
            Resolver.Log.Loglevel = Meadow.Logging.LogLevel.Trace;

            Resolver.Log.Info("Initializing hardware...");

            //==== instantiate the project lab hardware
            _projLab = ProjectLab.Create();

            Resolver.Log.Info($"Running on ProjectLab Hardware {_projLab.RevisionString}");

            //---- display controller (handles display updates)
            if (_projLab.Display is { } display)
            {
                Resolver.Log.Trace("Creating DisplayController");
                _displayController = new DisplayController(display);
                Resolver.Log.Trace("DisplayController up");
            }

            _environmentalSensor = new EnvironmentalSensor(_displayController);

            Resolver.Log.Info("Initialization complete");

            return base.Initialize();
        }

        public override Task Run()
        {
            Resolver.Log.Info("Run...");

            if (_displayController != null)
            {
                _displayController.Update();
            }

            _environmentalSensor.Poll();

            return base.Run();
        }
    }
}