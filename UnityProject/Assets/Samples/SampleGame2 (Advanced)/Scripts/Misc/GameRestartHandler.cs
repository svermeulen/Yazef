using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zenject.SpaceFighter
{
    public class GameRestartHandler : IInitializable, IDisposable, ITickable
    {
        readonly Settings _settings;
        readonly GameEvents _gameEvents;

        bool _isDelaying;
        float _delayStartTime;

        public GameRestartHandler(
            Settings settings, GameEvents gameEvents)
        {
            _settings = settings;
            _gameEvents = gameEvents;
        }

        public void Initialize()
        {
            _gameEvents.PlayerDiedSignal += OnPlayerDied;
        }

        public void Dispose()
        {
            _gameEvents.PlayerDiedSignal -= OnPlayerDied;
        }

        public void Tick()
        {
            if (_isDelaying)
            {
                if (Time.realtimeSinceStartup - _delayStartTime > _settings.RestartDelay)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        void OnPlayerDied()
        {
            // Wait a bit before restarting the scene
            _delayStartTime = Time.realtimeSinceStartup;
            _isDelaying = true;
        }

        [Serializable]
        public class Settings
        {
            public float RestartDelay;
        }
    }
}
