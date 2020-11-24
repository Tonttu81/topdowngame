using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ShootingScript shootingScript = collision.GetComponentInChildren<ShootingScript>();

            switch (tag)
            {
                case "ShotgunPowerUp":
                    shootingScript.ShotgunPowerUp();
                    break;
                case "ExplosiveBullets":
                    shootingScript.ExplosiveBulletsPowerUp();
                    break;
                case "RiflePowerUp":
                    shootingScript.RiflePowerUp();
                    break;
                case "SniperPowerUp":
                    shootingScript.SniperPowerUp();
                    break;
                case "GrenadeLauncher":
                    shootingScript.GrenadeLauncherPowerUp();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
