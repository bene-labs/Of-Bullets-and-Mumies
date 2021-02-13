using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;
    public int bulletsToShootAtOnes;
    public int Streuung = 60;
    private float scale;
    private float distanceToPlayer;
    public float sizze;
    public float mult;
    // Start is called before the first frame update
    void Start()
    {
        scale = this.transform.localScale.x;
        distanceToPlayer = (Player.transform.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(Player.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
        Vector3 vecMousePlayer =  mouseOnScreen - positionOnScreen;
        if (vecMousePlayer.magnitude < distanceToPlayer / 10)
        {
            return;
        }
        vecMousePlayer.Normalize();
        vecMousePlayer.z = 0;
        transform.position = vecMousePlayer * distanceToPlayer + Player.transform.position;
        //Ta Daaa

        if (positionOnScreen.x < mouseOnScreen.x)
        {
            transform.localScale = new Vector2(scale, -scale);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        else
        {
            transform.localScale = new Vector2(-scale, scale);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (bulletsToShootAtOnes == 1)
            {
                shoot(angle, vecMousePlayer);
            }
            else if (bulletsToShootAtOnes > 1)
            {
                shot(bulletsToShootAtOnes, angle, vecMousePlayer);
            }
        }
    }

    private void shot(int number, float angle, Vector3 vecMousePlayer)
    {
        float part = Streuung / (number - 1);
        for (int i = 0; i < number; i++)
        {
            //shoot(angle, vecMousePlayer - 30 + part);
            shoot(angle -30 + part * i, RotateVector(vecMousePlayer, -30 + part * i));
            //shoot(angle - 15, RotateVector(vecMousePlayer, -15));
        }
    }

    public Vector2 RotateVector(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    private void shoot(float angle, Vector3 vecMousePlayer)
    {
        sizze = this.GetComponent<SpriteRenderer>().sprite.bounds.extents.x * scale + Bullet.GetComponent<SpriteRenderer>().bounds.extents.x;
        mult = sizze / vecMousePlayer.magnitude;
        GameObject go = (GameObject)Instantiate(Bullet, transform.position + vecMousePlayer * mult , Quaternion.identity);
        bulletCreator bu = go.GetComponent<bulletCreator>();
        bu.isClone = true;
        bu.Player = Player;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 180));
        bu.scale = Bullet.GetComponent<bulletCreator>().scale;
        rb.velocity = vecMousePlayer * 2;
        go.SetActive(true);
    }
}
