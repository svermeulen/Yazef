namespace Zenject.SpaceFighter
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        readonly EnemyTunables _tunables;

        public EnemyInstaller(EnemyTunables tunables)
        {
            _tunables = tunables;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_tunables);

            Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();

            Container.Bind<EnemyStateIdle>().AsSingle();
            Container.Bind<EnemyStateAttack>().AsSingle();
            Container.Bind<EnemyStateFollow>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyDeathHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRotationHandler>().AsSingle();
        }
    }
}
