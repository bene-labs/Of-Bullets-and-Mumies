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
    public Animator anim;
    public float Life = 100;
    public float maxLife = 100;

    void Start()
    {
        scale = transform.localScale.x;
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Walk();
        Turn();
    }

    void Walk()
    {
        float y = 0;
        float x = 0;
        anim.SetBool("isWalking", false);
        if (Input.GetKey(KeyCode.W))
        {
            y += speed;
            anim.SetBool("isWalking", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            y -= speed;
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            x -= speed;
            anim.SetBool("isWalking", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += speed;
            anim.SetBool("isWalking", true);
        }
        rb.velocity = new Vector2(x, y);
    }

    void Turn()
    {
        if (((Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition)).x < Camera.main.WorldToViewportPoint(transform.position).x)
        {
            transform.localScale = new Vector2(scale, scale);
        }
        else
        {
            transform.localScale = new Vector2(-scale, scale);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == ("Button"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                col.gameObject.GetComponent<ButtonController>().isActivated = true;
            }
        }
    }
}
