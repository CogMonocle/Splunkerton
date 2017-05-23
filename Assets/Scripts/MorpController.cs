using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorpController : MonoBehaviour
{
    EnemyController enemy;
    Rigidbody2D morpRigidbody;
    Animator morpAnimator;
    bool facingRight;

    public LayerMask ground;
    public Transform groundChecker;
    public float maxSpeed;
    public float groundLookDistance;
    public float touchDamage;
    public float touchKnockback;
    
    void Start()
    {
        enemy = GetComponent<EnemyController>();
        morpRigidbody = GetComponent<Rigidbody2D>();
        morpAnimator = GetComponent<Animator>();
        facingRight = true;
    }
    
    void FixedUpdate()
    {
        if(enemy.ShouldDie && !enemy.Dead)
        {
            enemy.Die();
            DropHandler.instance.DropCoins(transform.position, 30, 50);
        }
        if (enemy.enableAI)
        {
            float xVel = maxSpeed;
            morpAnimator.SetFloat("Speed", xVel);
            if (!facingRight)
            {
                xVel *= -1;
            }
            morpRigidbody.velocity = new Vector2(xVel, morpRigidbody.velocity.y);
        }
    }

    void Update()
    {
        if (enemy.enableAI)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, groundLookDistance, ground);
            if (hit.collider == null)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D collision)
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
