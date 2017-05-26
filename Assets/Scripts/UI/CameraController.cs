using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController mainCam;

    Transform cameraTransform;
    Transform target;

    public Vector3 margin;
    public Vector3 maxDistance;
    public float percentDistancePerTick;
    public Rect cameraBounds;
    
    void Awake()
    {
        if (mainCam == null)
        {
            mainCam = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cameraTransform = this.GetComponent<Transform>();
        target = PlayerController.instance.transform;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = cameraTransform.localPosition;
        Vector3 error = target.localPosition - cameraTransform.localPosition;
        if(Mathf.Abs(error.x) > margin.x)
        {
            if(Mathf.Abs(error.x) > maxDistance.x)
            {
                newPosition.x += error.x - Mathf.Sign(error.x) * maxDistance.x;
                error.x = maxDistance.x;
            }
            newPosition.x += Mathf.Sign(error.x) * ((Mathf.Abs(error.x) - margin.x) * percentDistancePerTick);
        }
        if (Mathf.Abs(error.y) > margin.y)
        {
            if (Mathf.Abs(error.y) > maxDistance.y)
            {
                newPosition.y += error.y - Mathf.Sign(error.y) * maxDistance.y;
                error.y = maxDistance.y;
            }
            newPosition.y += Mathf.Sign(error.y) * ((Mathf.Abs(error.y) - margin.y) * percentDistancePerTick);
        }
        newPosition.x = Mathf.Max(Mathf.Min(newPosition.x, cameraBounds.xMax), cameraBounds.xMin);
        newPosition.y = Mathf.Max(Mathf.Min(newPosition.y, cameraBounds.yMax), cameraBounds.yMin);
        cameraTransform.localPosition = newPosition;
    }
}
