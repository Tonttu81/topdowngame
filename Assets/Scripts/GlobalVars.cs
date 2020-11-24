using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalVars : MonoBehaviour
{
    public static GlobalVars Instance;
    public float score;
    public float money;

    public bool startFade;
    public bool fadeScene;
    public float fadeTime;
    string targetScene;

    Image fade;

    GameObject player;

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
}
