namespace Zenject
{
    // For some platforms, it's desirable to be able to add dependencies to Zenject before
    // Unity even starts up (eg. WSA as described here https://github.com/svermeulen/Zenject/issues/118)
    // In those cases you can call StaticContext.Container.BindX to add dependencies
    // Anything you add there will then be injected everywhere, since all other contexts
    // should be children of StaticContext
    public static class StaticContext
    {
        static DiContainer _container;

#if UNITY_EDITOR
        // Required for disabling domain reload in enter the play mode feature. See: https://docs.unity3d.com/Manual/DomainReloading.html
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        public static void Clear()
        {
            _container = null;
        }

        public static bool HasContainer
        {
            get { return _container != null; }
        }

        public static DiContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new DiContainer();
                }

                return _container;
            }
        }
    }
}
