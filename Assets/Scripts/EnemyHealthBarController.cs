using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour
{
    private Camera mainCam;

    public EnemyController enemy;
    public Image fillImage;
    public Image bgImage;

    void Start()
    {
        mainCam = CameraController.mainCam.GetComponent<Camera>();
    }

    void Update()
    {
        float fill = enemy.Health / enemy.maxHealth;

        if(fill == 1)
        {
            fillImage.gameObject.SetActive(false);
            bgImage.gameObject.SetActive(false);
        }
        else
        {
            fillImage.gameObject.SetActive(true);
            bgImage.gameObject.SetActive(true);
        }

        fillImage.fillAmount = fill;

        transform.position = RectTransformUtility.WorldToScreenPoint(mainCam, enemy.transform.position) + enemy.healthBarPosition;
        
        if(enemy.Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
