using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform _bulletSpawnPoint; 
    [SerializeField] GameObject _bulletPrefab; 
    [SerializeField] float _bulletsPerMinute = 500f;

    [SerializeField]EntityType _entity = EntityType.Player;

    float _nextShot = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time >= _nextShot)
        {
            _nextShot = Time.time + (60f / _bulletsPerMinute);

            Projectile bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation).GetComponent<Projectile>();
            bullet.Initialize(_entity);
        }
    }
}
