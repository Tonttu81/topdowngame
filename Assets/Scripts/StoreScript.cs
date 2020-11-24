using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;

public class StoreScript : MonoBehaviour
{
    public static StoreScript Instance { get; private set; }

    float fadeTime = 7f;
    public bool storeOpen = false;

    public PostProcessVolume volume;
    DepthOfField dof;
    CanvasGroup store;

    public TextMeshProUGUI pointCounter;
    public PlayerScript playerScript;
    Button[] buttons;

    public ShootingScript shootingScript;
    public GameObject stats;
    TextMeshProUGUI[] statTexts; // tää seuraavaks

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        volume.profile.TryGetSettings(out dof);
        store = GetComponentInChildren<CanvasGroup>();
        buttons = store.gameObject.GetComponentsInChildren<Button>();
        statTexts = stats.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < buttons.Length; i++)
        {
            //print(buttons[i]);
            //print(statTexts[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (storeOpen)
            {
                CloseStore();
            }
            else
            {
                OpenStore();
            }
        }


        statTexts[4].text =
            shootingScript.bulletDamage.ToString("F2") + "\n" +
            shootingScript.bulletSpeed.ToString("F2") + "\n" +
            shootingScript.bulletPenetration + "\n" +
            shootingScript.bulletLife.ToString("F2") + "\n" +
            shootingScript.bullets + "\n" +
            shootingScript.fireRate.ToString("F2");

        if (shootingScript.grenadeLauncher)
        {
            statTexts[1].text = 
                "Explosion damage:\n" + 
                "Clusters:";

            statTexts[2].text =
                shootingScript.explosionDamage.ToString("F2") + "\n" +
                shootingScript.explosiveBullets;
        }
        else
        {
            statTexts[1].text = "";
            statTexts[2].text = "";
        }

        pointCounter.text = "Points: " + playerScript.storePoints;


        if (storeOpen)
        {
            store.alpha += fadeTime * Time.deltaTime;
            for (int i = 0; i < buttons.Length; i++)
            {
                // vaiha highlight väri punaseks jos pointsit ei riitä, vihreäks jos riittää
            }
        }
        else
        {
            store.alpha -= fadeTime * Time.deltaTime;
        }

        
    }

    void OpenStore()
    {
        dof.focusDistance.value = 0.1f;
        store.alpha = 0f;
        store.interactable = true;
        storeOpen = true;
    }

    void CloseStore()
    {
        dof.focusDistance.value = 10f;
        store.alpha = 1f;
        store.interactable = false;
        storeOpen = false;
    }
}
