using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public GameObject toDestroy;

    void OnTriggerEnter2D(Collider2D collision)
    {
        toDestroy.SetActive(false);
    }
}
