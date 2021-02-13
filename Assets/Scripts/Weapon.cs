using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;
    public string WeaponType = "shotgun";
    public int Streuung = 60;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
        Vector3 vecMousePlayer =  mouseOnScreen - positionOnScreen;
        if (vecMousePlayer.magnitude < 0.2)
            return;
        vecMousePlayer.Normalize();
        transform.position = vecMousePlayer * 0.5f + Player.transform.position;
        //Ta Daaa

        if (positionOnScreen.x < mouseOnScreen.x)
        {
            transform.localScale = new Vector3(scale, scale);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180 + angle));
        }
        else
        {
            transform.localScale = new Vector3(-scale, scale);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (WeaponType == "default")
            {
                shoot(angle, vecMousePlayer);
            }
            else if (WeaponType == "shotgun")
            {
                shot(4, angle, vecMousePlayer);
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
        GameObject go = (GameObject)Instantiate(Bullet, transform.position + vecMousePlayer, Quaternion.identity);
        go.GetComponent<bulletCreator>().isClone = true;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        go.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        rb.velocity = vecMousePlayer * 2;
        go.SetActive(true);
    }
}
