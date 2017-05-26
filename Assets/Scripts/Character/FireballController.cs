using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    float timeAlive;
    bool isAlive;
    Animator animator;
    Rigidbody2D fireballRigidbody;
    ParticleSystem particles;
    Projectile projectile;
    
    public float lifespan;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        fireballRigidbody = GetComponent<Rigidbody2D>();
        particles = GetComponentInChildren<ParticleSystem>();
        projectile = GetComponent<Projectile>();
    }

    void OnEnable()
    {
        timeAlive = 0;
        isAlive = true;
        projectile.IsAlive = true;
        if (particles != null)
        {
            particles.Play();
        }
    }
    
    void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive > lifespan && isAlive)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive)
        {
            PlayerController p = collision.GetComponent<PlayerController>();
            if (p == null && !collision.isTrigger)
            {
                isAlive = false;
                animator.SetTrigger("Dead");
                fireballRigidbody.velocity = Vector2.zero;
                particles.Stop();
            }
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
