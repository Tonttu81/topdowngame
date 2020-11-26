using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUp
{
    public string name;
    public int cost;
    public UnityEngine.Events.UnityAction method;

    public PowerUp(string _name, int _cost, UnityEngine.Events.UnityAction _method)
    {
        name = _name;
        cost = _cost;
        method = _method;
    }
}

public class PowerUpScript : MonoBehaviour
{
    PowerUp[] powerups = new PowerUp[5]; // Kun tekee uuden powerupin, tämä pittää laittaa isommaksi

    Button[] buttons;
    [SerializeField] ShootingScript shootingScript;
    [SerializeField] PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        CreatePowerUps();

        for (int i = 0; i < powerups.Length; i++)
        {
            buttons[i].onClick.AddListener(powerups[i].method); // Laittaa napille metodin
            TextMeshProUGUI text = buttons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>(); // Ottaa napin tekstin
            string p = (powerups[i].cost > 1) ? "points" : "point"; // Jos hinta on suurempi kuin 1, kirjoittaa "points" eikä "point"
            text.text = powerups[i].name + ":  " + powerups[i].cost + " " + p;
        }
    }

    private void Update()
    {
        if (StoreScript.Instance.storeOpen)
        {
            for (int i = 0; i < powerups.Length; i++)
            {
                if (playerScript.storePoints < powerups[i].cost)
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
    }

    void CreatePowerUps()
    {
        // Tähän voi lisätä poweruppeja tai vaihtaa niiden nimiä tai hintaa

        powerups[0] = new PowerUp("Rifle Power-Up", 1, RiflePowerUp);
        powerups[1] = new PowerUp("Shotgun Power-Up", 1, ShotgunPowerUp);
        powerups[2] = new PowerUp("Sniper Power-Up", 1, SniperPowerUp);
        powerups[3] = new PowerUp("Explosive Bullets", 3, ExplosiveBulletsPowerUp);
        powerups[4] = new PowerUp("Grenade Launcher", 5, GrenadeLauncherPowerUp);
    }


    // Power-Upit, täältä voi vaihtaa niitten statseja
    public void RiflePowerUp()
    {
        // Ammut nopeampaa, mutta vahinkosi kärsii hieman
        int cost = powerups[0].cost;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            shootingScript.bulletDamage *= 0.95f;
            shootingScript.fireRate *= 0.6f;
        }
    }

    public void ShotgunPowerUp()
    {
        // Haulikko-powerupin avulla ammut monta panosta yhdellä kerralla, mutta panosten matka lyhenee, alue mihin ammukset lentävät suurenee ja ammut hitaammin.
        int cost = powerups[1].cost;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            shootingScript.shotgunSpread *= 1.1f;
            shootingScript.bullets *= 2;
            shootingScript.bulletLife *= 0.4f;
            shootingScript.fireRate *= 1.1f;
            shootingScript.bulletDamage += 30f;
        }
    }

    public void SniperPowerUp()
    {
        // Ammut hitaammin, mutta teet enemmän vahinkoa, ammuksesi lentävät pidemmälle ja nopeampaa ja ammuksesi menevät vihollisista läpi
        int cost = powerups[2].cost;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            shootingScript.fireRate *= 1.5f;
            shootingScript.bulletDamage *= 1.6f;
            shootingScript.bulletLife *= 1.2f;
            shootingScript.bulletSpeed *= 1.5f;
            shootingScript.bulletPenetration++;
        }
    }

    public void ExplosiveBulletsPowerUp()
    {
        // Ammuksesi räjähtävät kun ne osuvat vihollisiin ja jokaisesta powerupista minkä keräät, saat lisävahinkoa
        int cost = powerups[3].cost;
        if (cost <= playerScript.storePoints)
        {
            playerScript.storePoints -= cost;
            shootingScript.explosiveBullets++;
            shootingScript.explosionRadius = 3f;
            shootingScript.explosionDamage *= 1.3f;
        }
    }

    public void GrenadeLauncherPowerUp()
    {
        // Kun saat kranaatinheittimen, ammuksesi räjähtävät kun ne osuvat vastustajaan, ja rähäjdyksistä tulee lisää räjähdyksiä, riippuen siitä kuinka monta
        // räjähtävää ammusta olet kerännyt. Ammuksesi kulkevat hitaammin
        int cost = powerups[4].cost;
        if (!shootingScript.grenadeLauncher)
        {
            if (cost <= playerScript.storePoints)
            {
                playerScript.storePoints -= cost;
                shootingScript.grenadeLauncher = true;
                shootingScript.bulletSpeed *= 0.7f;
                shootingScript.explosiveBullets += 2;
                shootingScript.explosionRadius = 3f;
            }   
        }
    }
}
