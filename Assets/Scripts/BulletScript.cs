using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float timer;
    int penetrations;

    Rigidbody2D rb2D;
    public GameObject explosion;
    ShootingScript shootingScript;

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ShootingScript>();

        transform.localScale = new Vector3(shootingScript.bulletSpeed * 0.01f, transform.localScale.y);
        timer = shootingScript.bulletLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (!StoreScript.Instance.storeOpen)
        {
            rb2D.velocity = transform.right * shootingScript.bulletSpeed;
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    Destroy(gameObject);
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
        if (collision.tag != "Player" || collision.tag != "Bullet")
        {
            if (collision.tag == "Enemy")
            {
                penetrations++;
                EnemyScript enemyScript = collision.GetComponent<EnemyScript>();
                enemyScript.TakeDamage(shootingScript.bulletDamage);
                Instantiate(explosion, transform.position, Quaternion.identity);
                if (penetrations > shootingScript.bulletPenetration)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
