using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float hp;
    public float damage;
    public float moveSpeed;
    public float baseXpReward;
    public float baseScoreReward;

    float gameTime; // Vastustajat scalee ajalla
    [SerializeField]float timer;

    // Shooting properties
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletLife;
    public float fireRate;

    GameObject player;
    PlayerScript playerScript;
    public GameObject floatingTextPrefab;
    public GameObject bullet;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    void Update()
    {
        if (player != null)
        {
            RotateTowardsPlayer();
        }

        if (timer <= 0)
        {
            Shoot();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            if (!StoreScript.Instance.storeOpen)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }
    }

    void Shoot()
    {
        GameObject shotBullet = Instantiate(bullet, transform.position, transform.rotation);
        shotBullet.GetComponent<BulletScript>().instantiatedByPlayer = false;
        timer = fireRate;
    }

    void RotateTowardsPlayer()
    {
        playerScript = player.GetComponent<PlayerScript>();
        if (!StoreScript.Instance.storeOpen)
        {
            Vector2 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        GlobalVars.Instance.InstantiateText("-" + damage.ToString("F2"), transform.position, true);

        if (hp <= 0)
        {
            playerScript.xp += baseXpReward / playerScript.level;
            playerScript.score += baseScoreReward * playerScript.scoreMultiplier;
            playerScript.killStreak++;
            if (playerScript.killStreak % 5 == 0)
            {
                if (playerScript.scoreMultiplier < 8)
                {
                    playerScript.scoreMultiplier *= 2;
                }
            }

            GlobalVars.Instance.InstantiateText("+" + (baseXpReward / playerScript.level).ToString("F2") + "xp", transform.position, false);

            Destroy(GetComponent<HpBar>().hpBar);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerScript>().TakeDamage(damage);
        }
    }
}
