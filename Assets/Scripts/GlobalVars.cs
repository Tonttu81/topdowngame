using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalVars : MonoBehaviour
{
    public static GlobalVars Instance { get; private set; }
    public float score;
    public float money;

    public bool startFade;
    public bool fadeScene;
    public float fadeTime;
    string targetScene;

    Vector3 direction;
    float timer;
    float dashDuration;
    bool dashing;

    public UnityEngine.Events.UnityAction selectedAbility;


    Image fade;

    GameObject player;

    public GameObject floatingTextPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        fadeScene = true;
        fade = GetComponentInChildren<Image>();
        
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (startFade)
        {
            if (fadeScene)
            {
                fade.color += new Color(0f, 0f, 0f, fadeTime * Time.deltaTime);
                if (fade.color.a >= 1)
                {
                    SceneManager.LoadScene(targetScene);
                    fadeScene = false;
                }
            }
            else
            {
                fade.color -= new Color(0, 0f, 0f, fadeTime * Time.deltaTime);
                if (fade.color.a < 0)
                {
                    fade.color = new Color(0f, 0f, 0f, 0f);
                    fadeScene = true;
                    startFade = false;
                }
            }
        }
    }


    public void ChangeScene(string scene)
    {
        targetScene = scene;
        startFade = true;
    }

    public void SaveVars()
    {
        score += player.GetComponent<PlayerScript>().score;
        money += player.GetComponent<PlayerScript>().score / 10;
    }

    public float LoadVars()
    {   
        return score;
    }

    // Luo leijuvan tekstin. Jos gravity on true, teksti lentää satunnaiseen suuntaan painovoima päällä. Jos gravity on false, leijuu suoraan ylöspäin hitaasti
    public void InstantiateText(string textInf, Vector3 position, bool gravity, float fontSize)  
    {
        GameObject floatingText = Instantiate(floatingTextPrefab, position, Quaternion.identity);
        FloatingTextScript floatingTextScript = floatingText.GetComponent<FloatingTextScript>();
        floatingTextScript.text.text = textInf;
        floatingTextScript.textGravity = gravity;
        floatingTextScript.notLaunched = gravity;
        floatingTextScript.text.fontSize = fontSize;
    }

    // Player abilities
    public void Shield()
    {
        print("Shield");
    }

    public void Healthpack()
    {
        float heal = 50f;
        player.GetComponent<PlayerScript>().hp += heal;
        InstantiateText("+" + heal, player.transform.position, false, 1);
    }

    public void Dash()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        dashing = true;
        // Dash in the direction

        print("Dash");
    }
}
