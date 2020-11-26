using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public int bullets = 1 ;
    public Vector2 shotgunSpread = new Vector2(-10f, 10f);
    public float bulletSpeed;
    public float bulletLife;
    public float bulletDamage;
    public int bulletPenetration;
    public float explosionDamage;
    public int explosiveBullets;
    public float explosionRadius;
    public float explosionScatter;
    public float fireRate;
    public bool grenadeLauncher;


    public float timer;

    public GameObject explosion;
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        if (!StoreScript.Instance.storeOpen)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if (Input.GetKeyDown("b"))
            {
                ReturnToDefaults();
            }
        }
    }

    void Shoot()
    {
        if (timer <= 0)
        {
            CameraScript.Instance.ShakeCamera((15f + bullets * 0.8f) * fireRate, 0.05f);
            if (bullets > 1)
            {
                for (int i = 0; i < bullets; i++)
                {
                    float bulletAngle = Random.Range(shotgunSpread.x, shotgunSpread.y);
                    GameObject shotBullet = Instantiate(bullet, transform.position, transform.parent.rotation * Quaternion.Euler(0f, 0f, bulletAngle));
                    shotBullet.GetComponent<BulletScript>().instantiatedByPlayer = true;
                }
            }
            else
            {
                GameObject shotBullet = Instantiate(bullet, transform.position, transform.parent.rotation);
                shotBullet.GetComponent<BulletScript>().instantiatedByPlayer = true;
            }
            timer = fireRate;
        }   
    }

    void ReturnToDefaults()
    {
        bullets = 1;
        bulletLife = 1f;
        bulletSpeed = 60f;
        shotgunSpread = new Vector2(-10f, 10f);
        explosiveBullets = 0;
        explosionRadius = 0.38f;
        explosionDamage = 40f;
        fireRate = 0.8f;
        bulletDamage = 20f;
        grenadeLauncher = false;
        bulletPenetration = 0;
    }
}
