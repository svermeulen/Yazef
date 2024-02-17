using System;
using UnityEngine;

namespace Zenject.Asteroids
{
    public class AudioHandler : IInitializable, IDisposable
    {
        readonly Settings _settings;
        readonly AudioSource _audioSource;
        readonly GameEvents _gameEvents;

        public AudioHandler(
            AudioSource audioSource,
            Settings settings,
            GameEvents gameEvents)
        {
            _settings = settings;
            _audioSource = audioSource;
            _gameEvents = gameEvents;
        }

        public void Initialize()
        {
            _gameEvents.ShipCrashedSignal += OnShipCrashed;
        }

        public void Dispose()
        {
            _gameEvents.ShipCrashedSignal -= OnShipCrashed;
        }

        void OnShipCrashed()
        {
            _audioSource.PlayOneShot(_settings.CrashSound);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip CrashSound;
        }
    }
}
