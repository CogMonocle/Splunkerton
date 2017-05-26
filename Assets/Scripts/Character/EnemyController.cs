using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICombatEntity
{
    bool shouldDie;
    bool dead;
    float health;
    float timeDead;
    Dictionary<int, bool> hitBy;

    public bool enableAI;
    public float deathVelocity;
    public float deathAngularVelocity;
    public float maxHealth;
    public float decayTime;
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
        timeDead = 0;
        hitBy = new Dictionary<int, bool>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            shouldDie = true;
        }
        if(dead)
        {
            timeDead += Time.deltaTime;
        }
        if(timeDead > decayTime)
        {
            Destroy(hpBar.gameObject);
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        dead = true;
        enableAI = false;
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            //c.isTrigger = true;
        }
        gameObject.layer = 18;
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
                Damage(j.damage);
                hitBy.Add(j.Id, true);
            }
        }
    }

    public void Damage(float amount)
    {
        health -= amount;
    }

    public void Heal(float amount)
    {
        health += amount;
    }

    public void Knockback(Vector3 source, float strength)
    {

    }
}
