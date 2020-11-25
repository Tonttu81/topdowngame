using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public int bullets = 1 ;
    Vector2 shotgunSpread = new Vector2(-10f, 10f);
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
    PlayerScript playerScript;

    private void Start()
    {
        playerScript = GetComponentInParent<PlayerScript>();
    }

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
        

    public void ShotgunPowerUp() 
    {
        int cost = 1;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            shotgunSpread *= 1.3f;
            bullets *= 2;
            bulletLife *= 0.4f;
            fireRate *= 1.1f;
        }
        // Haulikko-powerupin avulla ammut monta panosta yhdellä kerralla, mutta panosten matka lyhenee, alue mihin ammukset lentävät suurenee ja ammut hitaammin.
    }

    public void ExplosiveBulletsPowerUp()
    {
        int cost = 1;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            explosiveBullets++;
            explosionRadius = 3f;
            explosionDamage *= 1.3f;
        }
        // Ammuksesi räjähtävät kun ne osuvat vihollisiin ja jokaisesta powerupista minkä keräät, saat lisävahinkoa
    }

    public void GrenadeLauncherPowerUp()
    {
        int cost = 1;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            if (!grenadeLauncher)
            {
                grenadeLauncher = true;
                bulletSpeed *= 0.7f;
                explosiveBullets += 2;
                explosionRadius = 3f;
            }
        }
        // Kun saat kranaatinheittimen, ammuksesi räjähtävät kun ne osuvat vastustajaan, ja rähäjdyksistä tulee lisää räjähdyksiä, riippuen siitä kuinka monta
        // räjähtävää ammusta olet kerännyt. Ammuksesi kulkevat hitaammin
    }

    public void RiflePowerUp()
    {
        int cost = 1;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            bulletDamage *= 0.95f;
            fireRate *= 0.6f;
        }
        // Ammut nopeampaa, mutta vahinkosi kärsii hieman
    }

    public void SniperPowerUp()
    {
        int cost = 1;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            fireRate *= 1.5f;
            bulletDamage *= 1.6f;
            bulletLife *= 1.2f;
            bulletSpeed *= 1.5f;
            bulletPenetration++;
        }
        // Ammut hitaammin, mutta teet enemmän vahinkoa, ammuksesi lentävät pidemmälle ja nopeampaa ja ammuksesi menevät vihollisista läpi
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
