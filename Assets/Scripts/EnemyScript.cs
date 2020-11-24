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

    GameObject player;
    PlayerScript playerScript;
    public GameObject floatingTextPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    void Update()
    {
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
            if (!StoreScript.Instance.storeOpen)
            {
                Vector2 dir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
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

    public void TakeDamage(float damage)
    {
        hp -= damage;
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
            GameObject xp = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            xp.GetComponent<FloatingTextScript>().text.text = "+" + (baseXpReward / playerScript.level).ToString("F2") + "xp";
            if (xp != null)
            {
                Destroy(GetComponent<HpBar>().hpBar);
                Destroy(gameObject);
            } 
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
