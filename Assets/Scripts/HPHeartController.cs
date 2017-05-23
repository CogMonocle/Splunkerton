using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHeartController : MonoBehaviour
{
    Image heart;

    public float Fill
    {
        get
        {
            return heart.fillAmount;
        }

        set
        {
            heart.fillAmount = value;
        }
    }
    
    void Awake()
    {
        heart = GetComponent<Image>();
        
    }
}
