using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    float scoreValue;
    float moneyValue;


    public TextMeshProUGUI score;
    public TextMeshProUGUI money;
    CanvasGroup panel;
    GameObject player;
    PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<CanvasGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            scoreValue = playerScript.score;
            moneyValue = playerScript.score / 10;
        }
        
        if (player == null)
        {
            if (panel.alpha < 1)
            {
                panel.alpha += Time.deltaTime;
            }
            panel.interactable = true;
        }

        
        
        score.text = "Your score was " + scoreValue;
        money.text = "You earned " + moneyValue + "€";

        if (panel.interactable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GlobalVars.Instance.ChangeScene("MainMenu");
            }
        }
    }
}
