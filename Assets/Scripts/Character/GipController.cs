using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GipController : MonoBehaviour {

    EnemyController enemy;
    Rigidbody2D gipRigidbody;

    public float touchDamage;
    public float touchKnockback;

    void Start()
    {
        enemy = GetComponent<EnemyController>();
        gipRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (enemy.ShouldDie && !enemy.Dead)
        {
            enemy.Die();
            DropHandler.instance.DropCoins(transform.position, 30, 50);
        }
        if (enemy.enableAI)
        {

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Transform t = collision.transform.parent;
        PlayerController p = null;
        if (t != null)
        {
            p = t.GetComponent<PlayerController>();
        }
        if (p != null && !enemy.Dead)
        {
            p.Knockback(transform.position, touchKnockback);
            p.Damage(touchDamage);
        }
    }
}
