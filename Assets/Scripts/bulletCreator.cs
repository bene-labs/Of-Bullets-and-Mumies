using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCreator : MonoBehaviour
{
    public bool isClone = false;
    public int damage = 10;
    public int range = 10;
    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = new Vector2(0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > range || transform.position.y < -range || transform.position.x > range || transform.position.y < -range)
            Destroy(gameObject);
    }

    //rember to deal damage from here
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }*/
}
