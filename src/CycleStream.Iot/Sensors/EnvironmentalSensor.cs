using CycleStream.Iot.Mqtt;
using Meadow;
using Meadow.Foundation.Sensors.Atmospheric;
using Meadow.Units;
using System;
using System.Threading.Tasks;

namespace CycleStream.Iot.Sensors
{
    public class EnvironmentalSensor : ISensor
    {
        private Bme688 _bme688;
        private readonly DisplayController _displayController;
        private readonly MqttClient _mqttClient;

        public EnvironmentalSensor(DisplayController displayController, MqttClient mqttClient)
        {
            _displayController = displayController;
            _mqttClient = mqttClient;
            CreateI2CSensor();
            // EnableGasHeater();
        }

        public async Task Poll()
        {
            await _mqttClient.Start();

            var observer = Bme688.CreateObserver(
            handler: result =>
            {
                Resolver.Log.Info($"Observer: Temp changed by threshold; new temp: {result.New.Temperature?.Celsius:N2}C, old: {result.Old?.Temperature?.Celsius:N2}C");
            },
            filter: result =>
            {
                // c# 8 pattern match syntax. checks for !null and assigns var.
                if (result.Old?.Temperature is { } oldTemp &&
                result.Old?.Humidity is { } oldHumidity &&
                result.New.Temperature is { } newTemp &&
                result.New.Humidity is { } newHumidity)
                {
                    return (newTemp - oldTemp).Abs().Celsius > 0.5 && (newHumidity - oldHumidity).Percent > 0.05;
                }

                return false;
            }
            );

            _bme688?.Subscribe(observer);

            if (_bme688 != null)
            {
                _bme688.Updated += async (sender, result) =>
                {
                    Resolver.Log.Info($"  Temperature: {result.New.Temperature?.Celsius:N2}C");
                    Resolver.Log.Info($"  Relative Humidity: {result.New.Humidity:N2}%");
                    Resolver.Log.Info($"  Pressure: {result.New.Pressure?.Millibar:N2}mbar ({result.New.Pressure?.Pascal:N2}Pa)");
                    if (_bme688.GasConversionIsEnabled)
                    {
                        Resolver.Log.Info($"  Gas Resistance: {result.New.GasResistance:N0} Ohms");
                    }

                    if (_displayController != null)
                    {
                        _displayController.AtmosphericConditions = (result.New.Temperature, result.New.Humidity, result.New.Pressure, result.New.GasResistance);
                        _displayController.Update();
                    }

                    await _mqttClient.PublishMessage("Temperature", $"{result.New.Temperature?.Celsius:N2}");
                };
            }

            _bme688?.StartUpdating(TimeSpan.FromSeconds(10));
        }

        private void CreateI2CSensor()
        {
            Resolver.Log.Info("Create BME688 sensor with I2C...");

            _bme688 = new Bme688(Resolver.Device.CreateI2cBus(), (byte)Bme68x.Addresses.Address_0x76);
        }

        private void EnableGasHeater()
        {
            if (_bme688 != null)
            {
                _bme688.GasConversionIsEnabled = true;
                _bme688.HeaterIsEnabled = true;
                _bme688.ConfigureHeatingProfile(Bme68x.HeaterProfileType.Profile1, new Temperature(300), TimeSpan.FromMilliseconds(100), new Temperature(22));
                _bme688.HeaterProfile = Bme68x.HeaterProfileType.Profile1;
            }
        }
    }
}