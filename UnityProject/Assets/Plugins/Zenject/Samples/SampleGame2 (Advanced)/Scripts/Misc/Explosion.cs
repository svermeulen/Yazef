using UnityEngine;

#pragma warning disable 649

namespace Zenject.SpaceFighter
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]
        float _lifeTime;

        [SerializeField]
        ParticleSystem _particleSystem;

        float _startTime;

        public void Update()
        {
            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public void OnDespawned()
        {
        }

        [Inject]
        public void Construct()
        {
            _particleSystem.Clear();
            _particleSystem.Play();

            _startTime = Time.realtimeSinceStartup;
        }

        public class Factory : PlaceholderFactory<Explosion>
        {
        }
    }
}

