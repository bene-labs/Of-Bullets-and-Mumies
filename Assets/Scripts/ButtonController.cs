using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool isActivated;
    public Sprite onSprite;
    public Sprite offSprite;

    void Update()
    {
        if (isActivated)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
        else
            this.gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
    }
}
