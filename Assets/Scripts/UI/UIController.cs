using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    static bool paused;
    PlayerController player;

    public Canvas pauseMenu;
    public Inventory inventory;

    public static bool Paused
    {
        get
        {
            return paused;
        }
    }

    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = PlayerController.instance;
        paused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SetPaused(true);
        }
        if (Input.GetButton("Cancel"))
        {
            SetPaused(false);
        }

        if (!paused)
        {
            if (Input.GetButtonDown("Jump"))
            {
                player.Jump();
            }
            if (Input.GetButtonUp("Jump"))
            {
                player.EndJump();
            }
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    player.WeaponAttack();
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    player.CastSpell();
                }
            }
            if(Input.GetButtonDown("Inventory"))
            {
                inventory.ToggleVisible();
            }
        }
    }

    public void SetPaused(bool pause)
    {
        if (pause == paused)
        {
            return;
        }

        if (pause)
        {
            pauseMenu.enabled = true;
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            pauseMenu.enabled = false;
            Time.timeScale = 1;
            paused = false;
        }
    }
}
