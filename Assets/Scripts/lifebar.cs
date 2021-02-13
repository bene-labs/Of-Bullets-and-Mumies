using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifebar : MonoBehaviour
{
    public GameObject reference;
    public GameObject lifeCurrent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float life = getLife();
        float maxSize = getSize();
        float mult = maxSize / (lifeCurrent.GetComponent<SpriteRenderer>().sprite.bounds.extents.x);
        //transform.localScale = new Vector3(mult, 0.15f, 1);
        //transform.position = reference.transform.position + new Vector3(0, 0.3f, 1);
        lifeCurrent.transform.localScale = new Vector3(mult * (life / 100), 0.15f, 1);
        lifeCurrent.transform.position = reference.transform.position + new Vector3(0, 0.3f, 1);
    }

    private float getSize()
    {
        return reference.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
    }

    private float getLife()
    {
        if (reference.name == "Enemy")
        {
            return reference.GetComponent<EnemyController>().life;
        }
        else if (reference.name == "Player")
        {
            return reference.GetComponent<PlayerScript>().Life / reference.GetComponent<PlayerScript>().maxLife * 100;
        }
        return 0;
    }
}
