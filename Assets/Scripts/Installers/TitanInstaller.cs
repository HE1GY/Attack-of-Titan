using TitanFolder;
using UI;
using UnityEngine;
using Zenject;


namespace Installers
{
    public class TitanInstaller :MonoInstaller
    {
        [SerializeField] private TitanFolder.Titan _titanPrefab;
        [SerializeField] private TitanSpawner _titanSpawner;

        public override void InstallBindings()
        {
            Container.Bind<ITitanSpawner>()
                .To<TitanSpawner>()
                .FromComponentInNewPrefab(_titanSpawner)
                .AsSingle();
            
            Container.BindFactory<TitanFolder.Titan, TitanFolder.Titan.Factory>()
                .FromComponentInNewPrefab(_titanPrefab)
                .AsSingle();
        }
    }
}
