using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonArrowController : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject player;

    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    void Update()
    {
        foreach (GameObject button in buttons)
        {
            GameObject arrow = button.transform.GetChild(0).gameObject;
            if (!button.gameObject.GetComponent<ButtonController>().isActivated)
            {
                arrow.SetActive(true);

                float angle = Mathf.Atan2(player.transform.position.y - button.transform.position.y, player.transform.position.x - button.transform.position.x) * Mathf.Rad2Deg;
                Vector3 vecButtonPlayer = button.transform.position - player.transform.position;
                vecButtonPlayer.Normalize();
                vecButtonPlayer.z = 0;
                arrow.transform.position = vecButtonPlayer * 0.5f + player.transform.position;
                arrow.transform.localScale = new Vector2(-1, 1);
                arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                arrow.SetActive(false);
            }
        }
    }
}
