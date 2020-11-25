using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float timer;
    int penetrations;
    public bool instantiatedByPlayer;

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
            GetInstantiator();
        }

        if (!StoreScript.Instance.storeOpen)
        {
            SetVelocity();

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

    void SetVelocity()
    {
        if (instantiatedByPlayer)
        {
            rb2D.velocity = transform.right * shootingScript.bulletSpeed;
        }
        else
        {
            rb2D.velocity = transform.right * enemyScript.bulletSpeed;
        }
    }
    
    void GetInstantiator()
    {
        if (instantiatedByPlayer)
        {
            shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ShootingScript>();
            transform.localScale = new Vector3(shootingScript.bulletSpeed * 0.01f, transform.localScale.y);
            timer = shootingScript.bulletLife;
        }
        else
        {
            enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
            transform.localScale = new Vector3(enemyScript.bulletSpeed * 0.01f, transform.localScale.y);
            timer = enemyScript.bulletLife;
        }
        loaded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (instantiatedByPlayer)
        {
            if (collision.tag != "Player" || collision.tag != "Bullet")
            {
                if (collision.tag == "Enemy")
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
        }
        else
        {
            if (collision.tag != "Enemy" || collision.tag != "Bullet")
            {
                if (collision.tag == "Player")
                {
                    PlayerScript playerScriptInstance = collision.GetComponent<PlayerScript>();
                    playerScriptInstance.TakeDamage(enemyScript.bulletDamage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
