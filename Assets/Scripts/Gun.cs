using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform _bulletSpawnPoint; 
    [SerializeField] GameObject _bulletPrefab; 
    [SerializeField] float _bulletsPerMinute = 100;
    [SerializeField] float _range = 3;
    [SerializeField] LayerMask _enemyLayer;
    float _nextShot = 0;

    Collider2D _target;

    void Update()
    {
        CheckAndShoot();

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    /// need Targeting system, range, automatic shooting and targeting prefered
    void CheckAndShoot()
    {
        _target = Physics2D.OverlapCircle(transform.position, _range, _enemyLayer);
        
    }

    void Shoot()
    {
        if (Time.time >= _nextShot)
        {
            _nextShot = Time.time + (60f / _bulletsPerMinute);

            Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
