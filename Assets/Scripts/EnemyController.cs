using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool shouldDie;
    bool dead;
    float health;
    Dictionary<int, bool> hitBy;

    public bool enableAI;
    public float deathVelocity;
    public float deathAngularVelocity;
    public float maxHealth;
    public Vector2 healthBarPosition;
    public EnemyHealthBarController hpBar;

    public bool ShouldDie
    {
        get
        {
            return shouldDie;
        }
    }

    public bool Dead
    {
        get
        {
            return dead;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }
    }

    void Start()
    {
        shouldDie = false;
        dead = false;
        health = maxHealth;
        hpBar = EnemyHealthBarManager.instance.GetBar();
        hpBar.enemy = this;
        hitBy = new Dictionary<int, bool>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            shouldDie = true;
        }
    }

    public void Die()
    {
        dead = true;
        enableAI = false;
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.isTrigger = true;
        }

        Rigidbody2D r = GetComponent<Rigidbody2D>();
        r.velocity = deathVelocity * Vector2.up;
        r.AddTorque(deathAngularVelocity);
        r.constraints = RigidbodyConstraints2D.None;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile j = collision.GetComponent<Projectile>();
        if (j != null && j.IsAlive)
        {
            bool alreadyHit = false;
            hitBy.TryGetValue(j.Id, out alreadyHit);
            if (!alreadyHit)
            {
                TakeDamage(j.damage);
                hitBy.Add(j.Id, true);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
