using UnityEngine;
using Zenject;

namespace Svkj
{
    public class Foo : IInitializable
    {
        public void Initialize()
        {
            Debug.Log("created foo");
        }
    }

    public class SandboxInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Foo>().AsSingle();
        }
    }
}
