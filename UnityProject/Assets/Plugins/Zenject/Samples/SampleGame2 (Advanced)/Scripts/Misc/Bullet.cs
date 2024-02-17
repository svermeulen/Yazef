using UnityEngine;

namespace Zenject.SpaceFighter
{
    public enum BulletTypes
    {
        FromEnemy,
        FromPlayer
    }

    public class Bullet : MonoBehaviour
    {
        float _startTime;
        BulletTypes _type;
        Tunables _tunables;

        [SerializeField]
        MeshRenderer _renderer = null;

        [SerializeField]
        Material _playerMaterial = null;

        [SerializeField]
        Material _enemyMaterial = null;

        public BulletTypes Type
        {
            get { return _type; }
        }

        public Vector3 MoveDirection
        {
            get { return transform.right; }
        }

        public void OnTriggerEnter(Collider other)
        {
            var enemyView = other.GetComponent<EnemyView>();

            if (enemyView != null && _type == BulletTypes.FromPlayer)
            {
                enemyView.Facade.Die();
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                var player = other.GetComponent<PlayerFacade>();

                if (player != null && _type == BulletTypes.FromEnemy)
                {
                    player.TakeDamage(MoveDirection);
                    GameObject.Destroy(this.gameObject);
                }
            }
        }

        public void Update()
        {
            transform.position -= transform.right * _tunables.Speed * Time.deltaTime;

            if (Time.realtimeSinceStartup - _startTime > _tunables.Lifetime)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        [Inject]
        public void Construct(Tunables tunables, BulletTypes type)
        {
            _type = type;
            _tunables = tunables;

            _renderer.material = type == BulletTypes.FromEnemy ? _enemyMaterial : _playerMaterial;

            _startTime = Time.realtimeSinceStartup;
        }

        public class Tunables
        {
            public float Speed;
            public float Lifetime;
        }

        public class Factory : PlaceholderFactory<Tunables, BulletTypes, Bullet>
        {
        }
    }
}
