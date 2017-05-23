using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPopupController : MonoBehaviour
{
    float timeAlive;
    int value;

    public Text money;
    public Color positive;
    public Color negative;
    public float lifeTime;

    public int Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
            if(value >= 0)
            {
                money.text = "+ " + value;
                money.color = positive;
            }
            else if(value < 0)
            {
                money.text = "- " + value;
                money.color = negative;
            }
        }
    }

    public float TimeAlive
    {
        get
        {
            return timeAlive;
        }

        set
        {
            timeAlive = value;
        }
    }

    private void Awake()
    {
        money = GetComponent<Text>();
    }

    void OnEnable()
    {
        timeAlive = 0;
    }
    
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime)
        {
            //gameObject.SetActive(false);
            Value = 0;
        }
        else
        {
            Color c = money.color;
            c.a = 1 - timeAlive / lifeTime;
            money.color = c;
        }
    }
}
