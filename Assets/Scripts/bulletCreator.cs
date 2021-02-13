using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCreator : MonoBehaviour
{
    public bool isClone = false;
    public float scale = 1;
    public int damage = 10;
    public int range = 10;
    public GameObject Player;
    public Vector3 startPos = new Vector3();
    // Start is called before the first frame updatel
    void Start()
    {
        startPos = transform.position;
        scale = transform.localScale.x;
        //transform.localScale = new Vector2(0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((startPos - transform.position).magnitude > range)
            Destroy(gameObject);
    }

    private void DoDamage(EnemyController enemy)
    {
        enemy.life -= damage;
        Destroy(gameObject);
    }

    //rember to deal damage from here
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<bulletCreator>())
            Destroy(gameObject);
    }
}
