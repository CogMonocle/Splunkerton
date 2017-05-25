using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    SpriteRenderer sprite;
    float timeAlive;

    public AnimationCurve fade;
    public float timeScale;


    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        Color c = sprite.color;
        c.a = fade.Evaluate(timeAlive / timeScale);
        sprite.color = c;
        if(timeAlive > timeScale)
        {
            Destroy(gameObject);
        }
    }
}
