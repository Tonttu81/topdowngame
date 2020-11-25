using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextScript : MonoBehaviour
{
    public bool textGravity;
    Vector2 direction;
    public bool notLaunched;


    public TextMeshProUGUI text;
    GameObject canvas;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("WorldSpaceUI");
        text = GetComponent<TextMeshProUGUI>();
        rb2D = GetComponent<Rigidbody2D>();
        gameObject.transform.SetParent(canvas.transform, true);
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        text.color -= new Color(0f, 0f, 0f, Time.deltaTime);

        if (textGravity)
        {
            if (notLaunched)
            {
                rb2D.gravityScale = 1;
                rb2D.AddForce(direction * 10, ForceMode2D.Impulse);
                notLaunched = false;
            }
        }
        else
        {
            rb2D.gravityScale = 0;
            transform.position += Vector3.up * Time.deltaTime;
        }
        
        if (text.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
