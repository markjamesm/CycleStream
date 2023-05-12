using Meadow;
using Meadow.Devices;
using System.Threading.Tasks;
using CycleStream.Iot.Sensors;
using CycleStream.Iot.Mqtt;
using System.Collections.Generic;
using MQTTnet;

namespace CycleStream.Iot
{
    public class CycleStreamIot : App<F7FeatherV2>
    {
        private DisplayController _displayController;
        private IProjectLabHardware _projLab;
        private EnvironmentalSensor _environmentalSensor;
        private Accelerometer _accelerometer;

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

            var mqttFactory = new MqttFactory();
            var mqttClient = new MqttClient(mqttFactory);

           _environmentalSensor = new EnvironmentalSensor(_displayController, mqttClient);
           _accelerometer = new Accelerometer();

            Resolver.Log.Info("Initialization complete");

            return base.Initialize();
        }

        public override async Task Run()
        {
            Resolver.Log.Info("Run...");

            if (_displayController != null)
            {
                _displayController.Update();
            }

            await _environmentalSensor.Poll();
            _accelerometer.Poll();
        }
    }
}