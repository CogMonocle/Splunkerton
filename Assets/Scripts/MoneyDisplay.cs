using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    Text display;
    int money;

    //public ObjectPool popupPool;
    public MoneyPopupController popupDisplay;
    public float popupDisplacement;

    public int Money
    {
        get
        {
            return money;
        }

        // Single number popup
        set
        {
            int valueChange = value - money;
            if (valueChange == 0)
            {
                return;
            }
            money = value;
            display.text = money.ToString() + " M";
            popupDisplay.TimeAlive = 0;
            popupDisplay.Value += valueChange;
        }

        // Popup stream
        /*
        set
        {
            int valueChange = value - money;
            if(valueChange == 0)
            {
                return;
            }
            money = value;
            display.text = money.ToString() + " M";
            Vector3 position;
            foreach(GameObject g in popupPool)
            {
                if (g.activeInHierarchy)
                {
                    position = g.transform.position;
                    position.y -= popupDisplacement;
                    g.transform.position = position;
                }
            }
            GameObject popup = popupPool.getItem();
            popup.transform.localPosition = (Vector3.down * popupDisplacement);
            MoneyPopupController m = popup.GetComponent<MoneyPopupController>();
            m.Value = valueChange;
        }
        */
    }

    void Awake()
    {
        display = GetComponent<Text>();
    }

    void Update()
    {
        Vector3 v = popupDisplay.transform.localPosition;
        v.x = display.preferredWidth + popupDisplacement;
        popupDisplay.transform.localPosition = v;
    }
}
