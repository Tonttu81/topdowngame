using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float hp;
    public float score;
    public float level;
    public float xp;
    public float scoreMultiplier;
    public int killStreak;
    public int storePoints;

    
    
    // Start is called before the first frame update
    void Start()
    {
        scoreMultiplier = 1f;
        level = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (xp >= 100) // Jos on tarpeeksi xp, saa levelin
        {
            float remainder = xp - 100;
            xp = 0 + remainder;
            storePoints++;
            level++;
        }
        if (hp > 100)
        {
            hp = 100;
        }

        if (Input.GetButtonDown("Dash"))
        {
            GlobalVars.Instance.selectedAbility?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        scoreMultiplier = 1;
        killStreak = 0;
        GlobalVars.Instance.InstantiateText("-" + damage, transform.position, true, 1);
        CameraScript.Instance.ShakeCamera(10f, 0.1f);
        if (hp <= 0)
        {
            GlobalVars.Instance.SaveVars();
            GlobalVars.Instance.selectedAbility = null;
            Destroy(GetComponent<HpBar>().hpBar);
            Destroy(gameObject);
        }
    }
}