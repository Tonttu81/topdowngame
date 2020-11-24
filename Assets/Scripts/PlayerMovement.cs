using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    Vector2 axes;


    // Components
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StoreScript.Instance.storeOpen)
        {
            RotateCharacter();
        }
    }

    private void FixedUpdate()
    {
        if (!StoreScript.Instance.storeOpen)
        {
            Movement();
        }
        else
        {
            rb2D.velocity = new Vector2(0f, 0f);
        }
    }

    void Movement()
    {
        axes = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed * 100 * Time.deltaTime;
        rb2D.velocity = axes;
    }

    void RotateCharacter()
    {
        Vector3 mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        float rotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }
}
