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
    public bool bought;


    public Ability(string _name, float _cost, UnityEngine.Events.UnityAction _method, bool _bought)
    {
        name = _name;
        cost = _cost;
        method = _method;
        bought = _bought;
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
            text.text = abilities[i].name + ":  " + abilities[i].cost + "€"; // Vaihtaa tekstin
        }
    }

    // Update is called once per frame
    void Update()
    {
        globalVars = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVars>();

        for (int i = 0; i < abilities.Length; i++)
        {
            if (!abilities[i].bought) // Jos abilityä ei ole ostettu vielä
            {
                if (globalVars.money < abilities[i].cost) // Jos pelaajalla ei riitä rahat abilityyn, hightlight color on punainen
                {
                    ColorBlock colors = buttons[i].colors;
                    colors.highlightedColor = Color.red;
                    buttons[i].colors = colors;
                }
                else // Jos riittää rahat, highlight väri on vihreä
                {
                    ColorBlock colors = buttons[i].colors;
                    colors.highlightedColor = Color.green;
                    buttons[i].colors = colors;
                }
            }
            else // Jos ability on ostettu
            {
                if (abilities[i].method.Method.Name == globalVars.selectedAbility.Method.Name) // Jos ability on valittu, väri on musta
                {
                    ColorBlock colors = buttons[i].colors;
                    buttons[i].GetComponent<Image>().color = Color.black;
                    colors.highlightedColor = Color.black;
                    buttons[i].colors = colors;
                }
                else // Jos ability ei ole valittu, väri on valkoinen
                {
                    ColorBlock colors = buttons[i].colors;
                    buttons[i].GetComponent<Image>().color = Color.white;
                    colors.highlightedColor = Color.white;
                    buttons[i].colors = colors;
                }
            }
        }
    }

    void LoadAbilities()
    {
        abilities[0] = new Ability("Shield", 1, Shield, false);
        abilities[1] = new Ability("Healthpack", 1, Healthpack, false);
        abilities[2] = new Ability("Dash", 1, Dash, false);
    }

    // Tää seuraavaks
    void Shield()
    {
        float cost = abilities[0].cost;
        if (cost <= globalVars.money && !abilities[0].bought) // Jos pelaajalla riittää rahat abilityyn ja abilityä ei ole vielä ostettu
        {
            globalVars.money -= cost; // Vähentää pelaajalta raha
            abilities[0].bought = true;
            globalVars.selectedAbility = globalVars.Shield;
        }
        else if (cost > globalVars.money && !abilities[0].bought) // Jos pelaajalla ei riitä rahat ja abilityä ei ole vielä ostettu
        {
            globalVars.InstantiateText("Not enough money!", Input.mousePosition, false, 30); // Näyttää "Not enough money!" tekstin
        }
        else if (abilities[0].bought) // Jos ability on ostettu ja pelaaja klikkaa sitä
        {
            globalVars.selectedAbility = globalVars.Shield; // Laittaa abilityn selectedAbilityksi
        }
    }

    void Healthpack()
    {
        float cost = abilities[1].cost;
        if (cost <= globalVars.money && !abilities[1].bought)
        {
            globalVars.money -= cost;
            abilities[1].bought = true;
            globalVars.selectedAbility = globalVars.Healthpack;
        }
        else if (cost > globalVars.money && !abilities[1].bought)
        {
            globalVars.InstantiateText("Not enough money!", Input.mousePosition, false, 30);
        }
        else if (abilities[1].bought)
        {
            globalVars.selectedAbility = globalVars.Healthpack;
        }
    }

    void Dash()
    {
        float cost = abilities[2].cost;
        if (cost <= globalVars.money && !abilities[2].bought)
        {
            globalVars.money -= cost;
            abilities[2].bought = true;
            globalVars.selectedAbility = globalVars.Dash;
        }
        else if (cost > globalVars.money && !abilities[2].bought)
        {
            globalVars.InstantiateText("Not enough money!", Input.mousePosition, false, 30);
        }
        else if (abilities[2].bought)
        {
            globalVars.selectedAbility = globalVars.Dash;
        }
    }
}
