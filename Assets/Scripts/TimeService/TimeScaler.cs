
using System.Threading.Tasks;
using UnityEngine;

namespace TimeService
{
    public class TimeScaler :ITimeService
    {
        private const float DefaultTimeScale=1;
        private const float DefaultFixedDeltaTime = 0.02f;

        public void SlowDownTo(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = Time.timeScale*DefaultFixedDeltaTime;
        }

        public async void ReturnToDefault(float time)
        {
            await Returning(time);
        }

        private async Task Returning(float time)
        {
            while (Time.timeScale<DefaultTimeScale)
            {
                Time.timeScale +=DefaultTimeScale/ time * Time.unscaledDeltaTime;
                await Task.Yield();
            }
            Time.timeScale = DefaultTimeScale;
            Time.fixedDeltaTime = DefaultFixedDeltaTime;
        }
    }
}
