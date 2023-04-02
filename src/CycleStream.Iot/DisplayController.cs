using Meadow.Foundation;
using Meadow.Foundation.Graphics;

namespace CycleStream.Iot
{
    public class DisplayController
    {
        private readonly MicroGraphics _graphics;

        private bool _isUpdating = false;
        private bool _needsUpdate = false;

        public DisplayController(IGraphicsDisplay display)
        {
            _graphics = new MicroGraphics(display)
            {
                CurrentFont = new Font12x16()
            };

            _graphics.Clear(true);
        }

        public void Update()
        {
            if (_isUpdating)
            {   //queue up the next update
                _needsUpdate = true;
                return;
            }

            _isUpdating = true;

            _graphics.Clear();
            Draw();
            _graphics.Show();

            _isUpdating = false;

            if (_needsUpdate)
            {
                _needsUpdate = false;
                Update();
            }
        }

        private void Draw()
        {
            _graphics.DrawText(x: 2, y: 0, "Hello PROJ LAB!", WildernessLabsColors.AzureBlue);
        }
    }
}
