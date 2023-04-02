using Meadow;
using Meadow.Foundation;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Hardware;

namespace CycleStream.Iot
{
    public class DisplayController
    {
        MicroGraphics canvas;

        public DisplayController()
        {
            // this display needs mode3
            var config = new SpiClockConfiguration(new Meadow.Units.Frequency(12000, Meadow.Units.Frequency.UnitType.Kilohertz),
                SpiClockConfiguration.Mode.Mode3);

            // new up the display on the SPI bus
            var display = new St7789
            (
                spiBus: Resolver.Device.CreateSpiBus(Resolver.Device.GetPin("SCK"), Resolver.Device.GetPin("MOSI"), Resolver.Device.GetPin("MISO"), config),
                chipSelectPin: null,
                dcPin: Resolver.Device.GetPin("D01"),
                resetPin: Resolver.Device.GetPin("D00"),
                width: 240, height: 240
            );

            // create our graphics canvas that we'll draw onto 
            canvas = new MicroGraphics(display);

            // finally, clear any artifacts from the screen from boot up
            canvas.Clear(true);
        }

        public void Display()
        {
            canvas.CurrentFont = new Font12x20();
            canvas.DrawText(x: 5, y: 5, "hello, Meadow!", Color.Black);
        }
    }
}
