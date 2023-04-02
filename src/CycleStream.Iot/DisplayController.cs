using CycleStream.Iot;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Gateways.Bluetooth;
using Meadow.Hardware;
using Meadow.Peripherals.Displays;
using Meadow.Units;
using System;
using System.Threading;

namespace CycleStream.IoT
{
    public class DisplayController
    {
        St7789 st7789;
        MicroGraphics canvas;
        F7FeatherV2 _cycleStreamIot;

        public DisplayController(F7FeatherV2 cycleStreamIot)
        {
            _cycleStreamIot = cycleStreamIot;

            // this display needs mode3
            var config = new SpiClockConfiguration(new Meadow.Units.Frequency(12000, Meadow.Units.Frequency.UnitType.Kilohertz),
                SpiClockConfiguration.Mode.Mode3);

            // new up the display on the SPI bus
            var display = new St7789
            (
                device: _cycleStreamIot.Device,
                spiBus: _cycleStreamIot.Device.CreateSpiBus(_cycleStreamIot.Device.Pins.SCK, _cycleStreamIot.Device.Pins.MOSI, _cycleStreamIot.Device.Pins.MISO, config),
                chipSelectPin: null,
                dcPin: _cycleStreamIot.Device.Pins.D01,
                resetPin: _cycleStreamIot.Device.Pins.D00,
                width: 240, height: 240,
                displayColorMode: St7789._cycleStreamIot.Format16bppRgb565
            );

            // create our graphics canvas that we'll draw onto 
            canvas = new MicroGraphics(display);

            // finally, clear any artifacts from the screen from boot up
            canvas.Clear(true);
        }
    }
}
