using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnChance;
    public float spawnTime;

    [SerializeField]float timer;
    [SerializeField]float gameTime;

    public GameObject enemy;
    int random;

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if (spawnChance < 100)
        {
            spawnChance += gameTime * 0.0001f;
            
        }
        if (spawnTime > 2)
        {
            spawnTime -= gameTime * 0.00001f;
        }

        random = Random.Range(1, 100);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnTime;
            if (random <= spawnChance)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
            }
        }        
    }
}
