using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private EnemyController enemyInstance;

    public EnemyController enemyPrefab;

    public void Spawn()
    {
        enemyInstance = Instantiate(enemyPrefab, transform);
    }

    public void Despawn()
    {
        if (enemyInstance != null)
        {
            if (enemyInstance.hpBar != null)
            {
                Destroy(enemyInstance.hpBar.gameObject);
            }
            Destroy(enemyInstance.gameObject);
        }
    }
}
