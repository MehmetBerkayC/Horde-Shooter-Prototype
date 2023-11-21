using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletsPerMinute = 100;
    [SerializeField] private float _range = 3;
    [SerializeField] private int _damage = 10;
    [SerializeField] private LayerMask _enemyLayer;
    private float _nextShot = 0;

    private Transform _target;

    private void Update()
    {
        CheckAndShoot();
    }

    private void CheckAndShoot()
    {
        if (_target != null)
        {
            CheckDistance();
        }
        else
        {
            FindTarget();
        }
    }

    private void FindTarget()
    {
        // Search for a target
        Collider2D targetCandidate = Physics2D.OverlapCircle(transform.position, _range, _enemyLayer);
        if (targetCandidate != null)
        {
            if (targetCandidate.TryGetComponent(out Transform enemyTransform))
            {
                _target = enemyTransform;
            }
        }
    }

    private void CheckDistance()
    {
        if ((_target.transform.position - transform.position).magnitude > _range)
        {
            FindTarget();
        }
        else
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time >= _nextShot)
        {
            _nextShot = Time.time + (60 / _bulletsPerMinute);

            Projectile projectile = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            Vector3 shootDirection = (_target.position - transform.position).normalized;
            projectile.Setup(shootDirection, _damage, 10, 3f); // Change
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}