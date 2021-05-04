using System.ComponentModel;
using ModestTree;
#if !NOT_UNITY3D
using UnityEngine;

namespace Zenject
{
    public abstract class RunnableContext : Context
    {
        [Tooltip("When false, wait until run method is explicitly called. Otherwise run on initialize")]
        [SerializeField]
        bool _autoRun = true;

        static bool _staticAutoRun = true;

        public bool Initialized { get; private set; }
        
#if UNITY_EDITOR
        // Required for disabling domain reload in enter the play mode feature. See: https://docs.unity3d.com/Manual/DomainReloading.html
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStaticValues()
        {
            if (!UnityEditor.EditorSettings.enterPlayModeOptionsEnabled)
            {
                return;
            }
            
            _staticAutoRun = true;
        }
#endif

        protected override void Awake()
        {
            base.Awake();
            
#if UNITY_EDITOR
            if ((UnityEditor.EditorSettings.enterPlayModeOptions & UnityEditor.EnterPlayModeOptions.DisableSceneReload) != 0)
            {
                Initialized = false;
            }
#endif
        }

        protected void Initialize()
        {
            if (_staticAutoRun && _autoRun)
            {
                Run();
            }
            else
            {
                // True should always be default
                _staticAutoRun = true;
            }
        }

        public void Run()
        {
            Assert.That(!Initialized,
                "The context already has been initialized!");

            RunInternal();

            Initialized = true;
        }

        protected abstract void RunInternal();

        public static T CreateComponent<T>(GameObject gameObject) where T : RunnableContext
        {
            _staticAutoRun = false;

            var result = gameObject.AddComponent<T>();
            Assert.That(_staticAutoRun); // Should be reset
            return result;
        }
    }
}

#endif
