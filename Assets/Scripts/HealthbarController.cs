using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour {

    Stack<GameObject> hearts;
    int numHearts;

    public float healthPerHeart;
    public GameObject heartPrefab;
    public float heartSpacing;
    public float maxHearts;

    void Awake()
    {
        hearts = new Stack<GameObject>();
        numHearts = 0;
    }

    public void SetHealth(float health, float maxHealth)
    {
        GameObject activeHeart;
        while (numHearts * healthPerHeart < maxHealth)
        {
            activeHeart = Instantiate(heartPrefab, new Vector3(heartSpacing * numHearts + transform.position.x, transform.position.y), Quaternion.identity, transform);
            numHearts++;
            hearts.Push(activeHeart);
        }
        while ((numHearts - 1) * healthPerHeart > maxHealth)
        {
            activeHeart = hearts.Pop();
            Destroy(activeHeart);
            numHearts--;
        }
        GameObject[] heartArray = hearts.ToArray();
        float healthToShow = health;
        for(int i = numHearts; i > 0; i--)
        {
            HPHeartController filling = heartArray[i-1].GetComponentInChildren<HPHeartController>();
            float heartFill;
            if(healthToShow > healthPerHeart)
            {
                healthToShow -= healthPerHeart;
                heartFill = 1;
            }
            else if(healthToShow > 0)
            {
                heartFill = healthToShow / healthPerHeart;
                healthToShow = 0;
            }
            else
            {
                heartFill = 0;
            }
            filling.Fill = heartFill;
        }
    }
}
