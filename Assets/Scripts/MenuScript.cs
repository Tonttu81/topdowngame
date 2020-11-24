using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI moneyCounter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyCounter.text = "Money: " + GlobalVars.Instance.money.ToString("F2") + "€";
    }

    public void PlayGame()
    {
        GlobalVars.Instance.ChangeScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
