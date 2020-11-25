using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject hpBarPrefab;
    GameObject canvas;
    public GameObject hpBar;


    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("WorldSpaceUI");
        hpBar = Instantiate(hpBarPrefab, transform.position - new Vector3(0f, 1f), Quaternion.identity, canvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Slider hp = hpBar.GetComponent<Slider>();
        if (gameObject.tag == "Enemy")
        {
            hp.value = GetComponent<EnemyScript>().hp;
        }
        else
        {
            hp.value = GetComponent<PlayerScript>().hp;
        }
        hpBar.transform.position = transform.position - new Vector3(0f, 1f);
    }
}
