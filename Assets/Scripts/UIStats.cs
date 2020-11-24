using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStats : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI multiplier;

    TextMeshProUGUI[] statPanelTexts;
    Animator statPanelAnimator;
    public RectTransform statPanel;

    public TextMeshProUGUI level;
    public Slider hpBar;
    public Slider xpBar;
    public Slider reloadTimer;

    PlayerScript playerScript;
    ShootingScript shootingScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        shootingScript = playerScript.gameObject.GetComponentInChildren<ShootingScript>();
        statPanelTexts = statPanel.GetComponentsInChildren<TextMeshProUGUI>();
        statPanelAnimator = statPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = playerScript.hp;
        xpBar.value = playerScript.xp;
        level.text = "Level " + playerScript.level;
        score.text = "" + playerScript.score;
        if (playerScript.scoreMultiplier == 1)
        {
            multiplier.text = "";
        }
        else
        {
            multiplier.text = playerScript.scoreMultiplier + "x";
        }
        if (playerScript == null)
        {
            hpBar.value = 0;
        }

        if (!StoreScript.Instance.storeOpen)
        {
            if (Input.GetKeyDown("c"))
            {
                statPanelAnimator.Play("statPanel_open");
            }
            if (Input.GetKeyUp("c"))
            {
                statPanelAnimator.Play("statPanel_close");
            }
        }
        if (StoreScript.Instance.storeOpen)
        {
            statPanelAnimator.Play("statPanel_close");
        }

        reloadTimer.maxValue = shootingScript.fireRate;
        reloadTimer.value = shootingScript.timer;

        ChangeStats();
    }

    

    void ChangeStats()
    {
        statPanelTexts[2].text =
            shootingScript.bulletDamage.ToString("F2") + "\n" +
            shootingScript.bulletSpeed.ToString("F2") + "\n" +
            shootingScript.bulletPenetration + "\n" +
            shootingScript.bulletLife.ToString("F2") + "\n" + 
            shootingScript.bullets + "\n" +
            shootingScript.fireRate.ToString("F2");

        if (shootingScript.explosiveBullets > 0)
        {
            if (!shootingScript.grenadeLauncher)
            {
                statPanelTexts[4].text =
                shootingScript.explosionDamage.ToString("F2");

                statPanelTexts[3].text = "Explosion damage";
            }
            else
            {
                statPanelTexts[4].text =
                shootingScript.explosionDamage + "\n" +
                shootingScript.explosiveBullets;

                statPanelTexts[3].text = "Explosion damage\n" + "Clusters";
            }
        }
        else
        {
            statPanelTexts[3].text = "";
            statPanelTexts[4].text = "";
        }

        statPanelTexts[6].text = playerScript.xp.ToString("F2");
        
    }
}