using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float timer;
    int penetrations;
    public bool instantiatedByPlayer;

    float bulletSpeed;
    float bulletDamage;
    float bulletLife;
    int bulletPenetration;

    bool loaded;

    Rigidbody2D rb2D;
    public GameObject explosion;
    ShootingScript shootingScript;
    EnemyScript enemyScript;

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loaded)
        {
            GetBulletStats();
        }

        if (!StoreScript.Instance.storeOpen)
        {
            rb2D.velocity = transform.right * bulletSpeed;

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    DestroyBullet();
                }
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Bullet")
        {
            if (instantiatedByPlayer)
            {
                if (collision.tag != "Player")
                {
                    if (collision.tag == "Enemy")
                    {
                        HitEnemy(collision);
                    }
                }
            }
            else
            {
                if (collision.tag != "Enemy")
                {
                    if (collision.tag == "Player")
                    {
                        HitPlayer(collision);
                    }
                }
            }
        }
    }

    void GetBulletStats()
    {
        if (instantiatedByPlayer)
        {
            shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ShootingScript>();
            bulletDamage = shootingScript.bulletDamage;
            bulletLife = shootingScript.bulletLife;
            bulletPenetration = shootingScript.bulletPenetration;
            bulletSpeed = shootingScript.bulletSpeed;
            timer = shootingScript.bulletLife;
        }
        else
        {
            enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
            bulletDamage = enemyScript.bulletDamage;
            bulletLife = enemyScript.bulletLife;
            bulletSpeed = enemyScript.bulletSpeed;
            timer = enemyScript.bulletLife;
        }
        transform.localScale = new Vector3(bulletSpeed * 0.01f, transform.localScale.y);
        loaded = true;
    }

    void DestroyBullet()
    {
        if (instantiatedByPlayer)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void HitPlayer(Collider2D collision)
    {
        PlayerScript playerScriptInstance = collision.GetComponent<PlayerScript>();
        playerScriptInstance.TakeDamage(enemyScript.bulletDamage);
        Destroy(gameObject);
    }

    void HitEnemy(Collider2D collision)
    {
        penetrations++;
        EnemyScript enemyScriptInstance = collision.GetComponent<EnemyScript>();
        enemyScriptInstance.TakeDamage(shootingScript.bulletDamage);
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (penetrations > shootingScript.bulletPenetration)
        {
            Destroy(gameObject);
        }

    }
}
