using CycleStream.Iot.Mqtt;
using Meadow;
using Meadow.Foundation.Sensors.Accelerometers;
using Meadow.Units;
using System;
using System.Threading.Tasks;


namespace CycleStream.Iot.Sensors
{
    public class Accelerometer
    {
        private Bmi270 _bmi270;
        private readonly MqttClient _mqttClient;

        public Accelerometer() 
        { 
            _bmi270 = new Bmi270(Resolver.Device.CreateI2cBus());
        }


        public void Poll()
        {
            // Example that uses an IObservable subscription to only be notified when the filter is satisfied
            var consumer = Bmi270.CreateObserver(handler: result => HandleResult(this, result),
                                                 filter: result => FilterResult(result));

            _bmi270.Subscribe(consumer);

            _bmi270.StartUpdating(TimeSpan.FromMilliseconds(2000));
        }


        private bool FilterResult(IChangeResult<(Acceleration3D? Acceleration3D,
                                         AngularVelocity3D? AngularVelocity3D,
                                         Temperature? Temperature)> result)
        {
            return result.New.Acceleration3D.Value.Z > new Acceleration(0.1, Acceleration.UnitType.Gravity);
        }

        private void HandleResult(object sender,
                                  IChangeResult<(Acceleration3D? Acceleration3D,
                                  AngularVelocity3D? AngularVelocity3D,
                                  Temperature? Temperature)> result)
        {
            var accel = result.New.Acceleration3D.Value;
            var gyro = result.New.AngularVelocity3D.Value;
            var temp = result.New.Temperature.Value;

            Console.WriteLine($"AccelX={accel.X.Gravity:0.##}g, AccelY={accel.Y.Gravity:0.##}g, AccelZ={accel.Z.Gravity:0.##}g, GyroX={gyro.X.RadiansPerMinute:0.##}rpm, GyroY={gyro.Y.RadiansPerMinute:0.##}rpm, GyroZ={gyro.Z.RadiansPerMinute:0.##}rpm, {temp.Celsius:0.##}C");
        }
    }
}
