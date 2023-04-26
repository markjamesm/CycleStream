using System.Threading.Tasks;

namespace CycleStream.Iot.Sensors
{
    public interface ISensor
    {
        public Task Poll();
    }
}
