using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour, IEnumerable
{
    List<GameObject> pool;
    int lastItemIndex;

    public GameObject prefab;
    public int initialAmount;
    

    void Start()
    {
        pool = new List<GameObject>();
        lastItemIndex = 0;
        for(int i = 0; i < initialAmount; i++)
        {
            GameObject item = Instantiate(prefab, transform);
            Vector3 pos = item.transform.position + Vector3.forward * i;
            item.transform.position = pos;
            item.SetActive(false);
            pool.Add(item);
        }
    }

    public GameObject getItem()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            int index = (i + lastItemIndex) % pool.Count;
            if(!pool[index].activeInHierarchy)
            {
                lastItemIndex = index;
                pool[index].SetActive(true);
                return pool[index];
            }
        }
        GameObject item = Instantiate(prefab, transform);
        Vector3 pos = item.transform.position + Vector3.forward * pool.Count;
        item.transform.position = pos;
        pool.Add(item);
        return item;
    }

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)pool).GetEnumerator();
    }

    public GameObject this[int key]
    {
        get
        {
            return pool[key];
        }
    }
}
