using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    Projectile projectile;
    SpriteRenderer sprite;
    float timeAlive;

    public AnimationCurve fade;
    public float timeScale;


    void Start()
    {
        projectile = GetComponentInChildren<Projectile>();
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
