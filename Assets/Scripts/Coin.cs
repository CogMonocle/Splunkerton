using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;

    public Color Color
    {
        get
        {
            return GetComponent<SpriteRenderer>().color;
        }

        set
        {
            GetComponent<SpriteRenderer>().color = value;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Transform t = collision.transform.parent;
        PlayerController p = null;
        if (t != null)
        {
            p = t.GetComponent<PlayerController>();
        }
        if (p != null)
        {
            p.MoneyDollars += value;
            gameObject.SetActive(false);
        }
    }
}
