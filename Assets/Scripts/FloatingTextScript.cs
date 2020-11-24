using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    GameObject canvas;

    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("HealthBarUI");
        gameObject.transform.SetParent(canvas.transform, true);
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color -= new Color(0f, 0f, 0f, Time.deltaTime);
        transform.position += new Vector3(0f, 1f * Time.deltaTime);
        if (text.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
