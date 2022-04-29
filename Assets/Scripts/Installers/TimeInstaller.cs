using TimeService;
using Zenject;

namespace Installers
{
    public class TimeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeService>().To<TimeScaler>().AsSingle();
        }
    }
}