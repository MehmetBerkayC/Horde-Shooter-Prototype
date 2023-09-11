using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifles : MonoBehaviour
{
    [SerializeField] Transform _bulletSpawnPoint; 
    [SerializeField] GameObject _bulletPrefab; 
    [SerializeField] float _bulletsPerMinute = 100f;
    [SerializeField] float _lifeTime = 3f;

    float _nextShot = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextShot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        _nextShot += Time.time + (_bulletsPerMinute / 60f);

        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);

        Destroy(bullet, _lifeTime);
    }

}
