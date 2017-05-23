using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandler : MonoBehaviour
{
    public static DropHandler instance;

    public Coin[] coinTypes;
    public int coinFluffing;
    public float spawnForceMagnitude;
    public ObjectPool coinPool;

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

    public void SpawnCoin(Vector2 position, Coin prefab)
    {
        Vector2 v = Random.insideUnitCircle;
        v *= spawnForceMagnitude;
        GameObject item = coinPool.getItem();
        item.transform.position = new Vector3(position.x, position.y, item.transform.position.z);
        item.transform.localScale = prefab.transform.localScale;
        Coin c = item.GetComponent<Coin>();
        c.value = prefab.value;
        c.Color = prefab.Color;
        Rigidbody2D body = item.GetComponent<Rigidbody2D>();
        body.AddForce(v, ForceMode2D.Impulse);
    }

    public void SpawnItem(Vector2 position, GameObject prefab)
    {
        Vector2 v = Random.insideUnitCircle;
        v *= spawnForceMagnitude;
        GameObject item = Instantiate(prefab, position, Quaternion.identity, this.transform);
        Rigidbody2D body = item.GetComponent<Rigidbody2D>();
        body.AddForce(v, ForceMode2D.Impulse);
    }

    public void DespawnDrops()
    {
        foreach (Coin c in GetComponentsInChildren<Coin>())
        {
            c.gameObject.SetActive(false);
        }
    }

    public void DropCoins(Vector2 position, int min, int max)
    {
        int value = Random.Range(min, max + 1);
        foreach (Coin c in coinTypes)
        {
            if (c.value > 1)
            {
                while (value >= (coinFluffing + 1) * c.value)
                {
                    SpawnCoin(position, c);
                    value -= c.value;
                }
            }
            else
            {
                while (value > 0)
                {
                    SpawnCoin(position, c);
                    value--;
                }
            }
        }
    }
}
