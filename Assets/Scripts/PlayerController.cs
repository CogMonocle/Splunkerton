using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public enum Stats
    {
        None,
        Strength,
        Dexterity,
        Intelligence,
        Toughness,
        Luck,
        Wisdom
    }

    // Private members

    // -Other gameobjects
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;

    // -Controls and movement
    bool facingRight;
    bool isGrounded;
    bool isJumping;
    bool knocked;
    bool dead;
    float damageTimeout;
    int updatesSinceKnock;

    // -Stats
    float health;
    float maxHealth;
    int moneyDollars;
    Dictionary<Stats, int> stats;

    // -Equipment and inventory
    List<IInventoryItem> inventory;

    //Public members

    // -Controls and movement
    public float maxSpeed;
    public float jumpForce;
    public Transform groundSensor;
    public float groundRadius;
    public LayerMask groundLayers;
    public float fireballSpeed;
    public ObjectPool fireballPool;
    public float damageTimeoutLength;
    public int knockFrameSkip;

    // -Body
    public Equipment hand1;

    // -Stats
    public HealthbarController healthBar;
    public MoneyDisplay money;

    public Equipment ironSword;
    public SwordSlash slashEffect;
    public GameObject effectContainer;

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = Mathf.Min(Mathf.Max(value, 0f), MaxHealth);
            if (health == 0)
            {
                dead = true;
            }
            healthBar.SetHealth(Health, MaxHealth);
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = Mathf.Max(value, 0f);
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            healthBar.SetHealth(Health, MaxHealth);
        }
    }

    public int MoneyDollars
    {
        get
        {
            return moneyDollars;
        }

        set
        {
            moneyDollars = value;
            money.Money = moneyDollars;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        facingRight = true;
        isGrounded = false;
        isJumping = false;
        dead = false;
        MaxHealth = 300f;
        Health = MaxHealth;
        damageTimeout = 0;
        updatesSinceKnock = 0;
        MoneyDollars = 0;
        Equip(ironSword);
    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundSensor.position, groundRadius, groundLayers);
        updatesSinceKnock++;
        if (damageTimeout < 0 || (isGrounded && updatesSinceKnock >= knockFrameSkip))
        {
            knocked = false;
        }

        if (!knocked)
        {
            float move = Input.GetAxis("Horizontal");

            playerAnimator.SetFloat("Speed", Mathf.Abs(move));

            Vector2 velocity = playerRigidBody.velocity;
            velocity.x = move * maxSpeed;
            playerRigidBody.velocity = velocity;

            if (move != 0 && (move > 0 != facingRight))
                Flip();
        }
        playerAnimator.SetFloat("vSpeed", playerRigidBody.velocity.y);
        playerAnimator.SetBool("Grounded", isGrounded);
    }

    void Update()
    {
        damageTimeout -= Time.deltaTime;
        float damageWeight = damageTimeout > 0 ? 1f : 0f;
        playerAnimator.SetLayerWeight(1, damageWeight);
    }

    public void Jump()
    {
        if (isGrounded && !isJumping && !knocked)
        {
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            isJumping = true;
        }
    }

    public void EndJump()
    {
        if (isJumping && !knocked)
        {
            Vector2 velocity = playerRigidBody.velocity;
            velocity.y = Mathf.Min(velocity.y, 0f);
            playerRigidBody.velocity = velocity;
            isJumping = false;
        }
    }

    public void WeaponAttack()
    {
        SwordSlash s = Instantiate(slashEffect, effectContainer.transform);
        Vector3 direction = Input.mousePosition - CameraController.mainCam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
        direction.z = 0;
        s.transform.localRotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    public void CastSpell()
    {
        GameObject newFireball = fireballPool.getItem();
        newFireball.transform.localPosition = fireballPool.transform.localPosition;
        Vector3 direction = Input.mousePosition - CameraController.mainCam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
        direction.z = 0;
        Vector2 velocity = new Vector2(direction.x, direction.y);
        velocity.Normalize();
        newFireball.transform.localPosition += new Vector3(velocity.x, velocity.y);
        velocity *= fireballSpeed;
        newFireball.GetComponent<Rigidbody2D>().velocity = velocity;
        newFireball.transform.localRotation = Quaternion.FromToRotation(Vector3.right, direction);
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        Vector3 effectScale = effectContainer.transform.localScale;
        effectScale.x *= -1;
        effectContainer.transform.localScale = effectScale;
    }

    public void Damage(float amount)
    {
        if (damageTimeout < 0)
        {
            Health -= amount;
            damageTimeout = damageTimeoutLength;
        }
    }

    public void Knockback(Vector3 source, float strength)
    {
        Knockback(new Vector2(source.x, source.y), strength);
    }

    public void Knockback(Vector2 source, float strength)
    {
        if (damageTimeout < 0)
        {
            playerRigidBody.AddForce(new Vector2(strength * Mathf.Sign(transform.position.x - source.x), strength), ForceMode2D.Impulse);
            isGrounded = false;
            knocked = true;
            updatesSinceKnock = 0;
        }
    }

    public void Equip(Equipment e)
    {
        Equipment equipment = null;
        switch (e.itemSlot)
        {
            case Equipment.Slot.Mainhand:
                equipment = hand1;
                break;
        }
        if (equipment != null)
        {
            if (equipment.value >= 0)
            {
                inventory.Add(equipment);
            }
            equipment.SetInfo(e);
        }
    }
}
