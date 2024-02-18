using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    // Main installer for our game
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();

            Container.BindFactory<EnemyTunables, EnemyFacade, EnemyFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<EnemyInstaller>(_settings.EnemyFacadePrefab)
                // Place each enemy under an Enemies game object at the root of scene hierarchy
                .UnderTransformGroup("Enemies");

            Container.BindFactory<Bullet.Tunables, BulletTypes, Bullet, Bullet.Factory>()
                // Bullets are simple enough that we don't need to make a subcontainer for them
                // The logic can all just be in one class
                .FromComponentInNewPrefab(_settings.BulletPrefab)
                .UnderTransformGroup("Bullets");

            Container.Bind<LevelBoundary>().AsSingle();

            Container.BindFactory<Explosion, Explosion.Factory>()
                .FromComponentInNewPrefab(_settings.ExplosionPrefab)
                .UnderTransformGroup("Explosions");

            Container.Bind<AudioPlayer>().AsSingle();

            Container.BindInterfacesTo<GameRestartHandler>().AsSingle();

            Container.Bind<EnemyRegistry>().AsSingle();
            Container.Bind<GameEvents>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public GameObject EnemyFacadePrefab;
            public GameObject BulletPrefab;
            public GameObject ExplosionPrefab;
        }
    }
}
