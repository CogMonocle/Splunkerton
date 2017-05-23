using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    static int nextID = 0;

    bool isAlive;
    int id;

    public float damage;

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }
    }

    void OnEnable()
    {
        IsAlive = true;
        id = nextID;
        nextID++;
    }
}
