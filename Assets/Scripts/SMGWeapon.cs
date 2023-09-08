using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGWeapon : MonoBehaviour
{
    public Transform bulletSpawnPoint; 
    public GameObject bulletPrefab; 
    public float fireRate = 10f; 
    private float timeToFire = 0f;

    void Update()
    {
        
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    { 
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        Destroy(bullet, 3f);
    }

}
