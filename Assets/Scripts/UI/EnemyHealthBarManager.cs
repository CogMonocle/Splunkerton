using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    public static EnemyHealthBarManager instance;

    public EnemyHealthBarController barPrefab;

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

    public EnemyHealthBarController GetBar()
    {
        EnemyHealthBarController h = Instantiate(barPrefab, transform);
        return h;
    }
}
