using System;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class EnemyDeathHandler
    {
        readonly Settings _settings;
        readonly Explosion.Factory _explosionFactory;
        readonly AudioPlayer _audioPlayer;
        readonly EnemyView _view;
        readonly GameEvents _gameEvents;
        readonly EnemyFacade _facade;

        public EnemyDeathHandler(
            EnemyView view,
            AudioPlayer audioPlayer,
            Explosion.Factory explosionFactory,
            Settings settings,
            EnemyFacade facade,
            GameEvents gameEvents)
        {
            _facade = facade;
            _settings = settings;
            _explosionFactory = explosionFactory;
            _audioPlayer = audioPlayer;
            _view = view;
            _gameEvents = gameEvents;
        }

        public void Die()
        {
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _view.Position;

            _audioPlayer.Play(_settings.DeathSound, _settings.DeathSoundVolume);

            _gameEvents.EnemyKilledSignal();

            _facade.Dispose();
        }

        [Serializable]
        public class Settings
        {
            public AudioClip DeathSound;
            public float DeathSoundVolume = 1.0f;
        }
    }
}
