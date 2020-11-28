using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class Ability
{
    public string name;
    public float cost;
    public UnityEngine.Events.UnityAction method;


    public Ability(string _name, float _cost, UnityEngine.Events.UnityAction _method)
    {
        name = _name;
        cost = _cost;
        method = _method;
    }

    public void Shield()
    {

    }

    public void HealthPack()
    {

    }
    
    public void Dash()
    {

    }
}

public class UpgradeStore : MonoBehaviour
{
    Ability[] abilities = new Ability[3];


    GlobalVars globalVars;
    Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        globalVars = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVars>();
        buttons = GetComponentsInChildren<Button>();
        LoadAbilities();

        for (int i = 0; i < abilities.Length; i++)
        {
            buttons[i].onClick.AddListener(abilities[i].method); // Laittaa napille metodin
            TextMeshProUGUI text = buttons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>(); // Ottaa napin tekstin
            text.text = abilities[i].name + ":  " + abilities[i].cost + "€";
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (globalVars.money < abilities[i].cost)
            {
                ColorBlock colors = buttons[i].colors;
                colors.highlightedColor = Color.red;
                buttons[i].colors = colors;
            }
            else
            {
                ColorBlock colors = buttons[i].colors;
                colors.highlightedColor = Color.green;
                buttons[i].colors = colors;
            }
        }
    }

    void LoadAbilities()
    {
        abilities[0] = new Ability("Shield", 5000, Shield);
        abilities[1] = new Ability("Healthpack", 10000, Healthpack);
        abilities[2] = new Ability("Dash", 600, Dash);
    }

    // Tää seuraavaks
    void Shield()
    {
        print("shield");
    }

    void Healthpack()
    {
        print("healthpack");
    }

    void Dash()
    {
        print("dash");
    }
}
