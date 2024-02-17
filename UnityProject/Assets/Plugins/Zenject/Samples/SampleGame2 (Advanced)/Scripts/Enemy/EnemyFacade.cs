using UnityEngine;
using System;

namespace Zenject.SpaceFighter
{
    // Here we can add some high-level methods to give some info to other
    // parts of the codebase outside of our enemy facade
    public class EnemyFacade : MonoBehaviour, IDisposable
    {
        EnemyView _view;
        EnemyTunables _tunables;
        EnemyDeathHandler _deathHandler;
        EnemyStateManager _stateManager;
        EnemyRegistry _registry;

        [Inject]
        public void Construct(
            EnemyView view,
            EnemyTunables tunables,
            EnemyDeathHandler deathHandler,
            EnemyStateManager stateManager,
            EnemyRegistry registry)
        {
            _view = view;
            _tunables = tunables;
            _deathHandler = deathHandler;
            _stateManager = stateManager;
            _registry = registry;
        }

        public EnemyStates State
        {
            get { return _stateManager.CurrentState; }
        }

        public float Accuracy
        {
            get { return _tunables.Accuracy; }
        }

        public float Speed
        {
            get { return _tunables.Speed; }
        }

        public Vector3 Position
        {
            get { return _view.Position; }
            set { _view.Position = value; }
        }

        public void Die()
        {
            _deathHandler.Die();
        }

        public void Dispose()
        {
            GameObject.Destroy(this.gameObject);
        }

        public void OnDespawned()
        {
            _registry.RemoveEnemy(this);
        }

        public class Factory : PlaceholderFactory<EnemyTunables, EnemyFacade>
        {
        }
    }
}
