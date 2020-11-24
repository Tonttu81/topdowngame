using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float explosionDamage;
    public float explosionRadius;
    public int explosions;
    public bool clusters;
    public int explosiveBullets;
    public float explosionScatter;
    private float explosionTime = 1f;
    private float timer;
    public bool grenadeLauncher;
    

    public LayerMask enemyLayer;
    ShootingScript shootingScript;
    GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingScript = player.GetComponentInChildren<ShootingScript>();
        if (!clusters)
        {
            SetVars();
        }
        clusters = true;
        explosions++;
        transform.localScale = new Vector2(explosionRadius, explosionRadius);
        timer = explosionTime;
        if (explosiveBullets > 0)
        {
            CameraScript.Instance.ShakeCamera(5f * (explosionDamage * 0.05f) / (Vector2.Distance(transform.position, player.transform.position) * 0.7f), 0.1f);
            RaycastHit2D[] explosion = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero, 0f, enemyLayer);
            for (int i = 0; i < explosion.Length; i++)
            {
                explosion[i].collider.gameObject.GetComponent<EnemyScript>().TakeDamage(explosionDamage);
            }
            if (grenadeLauncher)
            {
                Invoke("Cluster", 0.1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Cluster()
    {
        if (explosions < explosiveBullets)
        {
            explosionRadius -= 0.5f;
            explosionDamage *= 0.5f;
            explosionScatter *= 1.5f;
            Vector3 explosion1Offset = new Vector3(Random.Range(-explosionScatter, explosionScatter), Random.Range(-explosionScatter, explosionScatter));
            Vector3 explosion2Offset = new Vector3(Random.Range(-explosionScatter, explosionScatter), Random.Range(-explosionScatter, explosionScatter));
            Instantiate(gameObject, transform.position - explosion1Offset, Quaternion.identity);
            Instantiate(gameObject, transform.position + explosion2Offset, Quaternion.identity);
        }
    }
    
    void SetVars()
    {
        explosionDamage = shootingScript.explosionDamage;
        explosionRadius = shootingScript.explosionRadius;
        explosiveBullets = shootingScript.explosiveBullets;
        explosionScatter = shootingScript.explosionScatter;
        grenadeLauncher = shootingScript.grenadeLauncher;
    }
}
