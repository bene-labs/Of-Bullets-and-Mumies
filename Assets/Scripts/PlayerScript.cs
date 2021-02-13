using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public string direction = "right";
    public float speed = 0.2f;
    private float scale;
    public Rigidbody2D rb;
    public GameObject cam;

    void Start()
    {
        scale = transform.localScale.x;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        float y = 0;
        float x = 0;
        if (Input.GetKey(KeyCode.W))
        {
            y += speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y -= speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            x -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += speed;
        }
        rb.velocity = new Vector2(x, y);

        if (((Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition)).x < Camera.main.WorldToViewportPoint(transform.position).x)
        {
            transform.localScale = new Vector2(scale, scale);
        }
        else
        {
            transform.localScale = new Vector2(-scale, scale);
        }
    }
}
