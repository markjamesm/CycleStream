using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using Meadow.Units;

namespace CycleStream.Iot
{
    public class DisplayController
    {
        readonly MicroGraphics graphics;

        bool isUpdating = false;
        bool needsUpdate = false;

        public (Temperature? Temperature, RelativeHumidity? Humidity, Pressure? Pressure, Resistance? GasResistance)? AtmosphericConditions
        {
            get => atmosphericConditions;
            set
            {
                atmosphericConditions = value;
                Update();
            }
        }
        (Temperature? Temperature, RelativeHumidity? Humidity, Pressure? Pressure, Resistance? GasResistance)? atmosphericConditions;

        public DisplayController(IGraphicsDisplay display)
        {
            graphics = new MicroGraphics(display)
            {
                CurrentFont = new Font12x16()
            };

            graphics.Clear(true);
        }

        public void Update()
        {
            if (isUpdating)
            {   //queue up the next update
                needsUpdate = true;
                return;
            }

            isUpdating = true;

            graphics.Clear();
            Draw();
            graphics.Show();

            isUpdating = false;

            if (needsUpdate)
            {
                needsUpdate = false;
                Update();
            }
        }

        void DrawStatus(string label, string value, Color color, int yPosition)
        {
            graphics.DrawText(x: 20, y: yPosition, label, color: color);
            graphics.DrawText(x: 238, y: yPosition, value, alignmentH: HorizontalAlignment.Right, color: color);
        }

        void Draw()
        {
            graphics.DrawText(x: 25, y: 0, "CycleStream", WildernessLabsColors.AzureBlue);

            if (AtmosphericConditions is { } conditions)
            {
                if (conditions.Temperature is { } temp)
                {
                    DrawStatus("Temperature:", $"{temp.Celsius:N1}C", WildernessLabsColors.GalleryWhite, 35);
                }

                if (conditions.Pressure is { } pressure)
                {
                    DrawStatus("Pressure:", $"{pressure.StandardAtmosphere:N1}atm", WildernessLabsColors.GalleryWhite, 55);
                }

                if (conditions.Humidity is { } humidity)
                {
                    DrawStatus("Humidity:", $"{humidity.Percent:N1}%", WildernessLabsColors.GalleryWhite, 75);
                }

                if (conditions.GasResistance is { } gasResistance)
                {
                    DrawStatus("Gas Res:", $"{gasResistance.Ohms:N0}", WildernessLabsColors.GalleryWhite, 95);
                }
            }
        }
    }
}