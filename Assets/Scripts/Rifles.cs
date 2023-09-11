using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifles : MonoBehaviour
{
    [SerializeField] Transform _bulletSpawnPoint; 
    [SerializeField] GameObject _bulletPrefab; 
    [SerializeField] float _bulletsPerMinute = 500f;
    [SerializeField] float _lifeTime = 3f;

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

            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

            Destroy(bullet, _lifeTime);
        }
    }
}
