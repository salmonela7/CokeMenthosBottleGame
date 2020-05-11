using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!PointIsVisibleToCamera(transform.position)) Destroy(gameObject);
    }
    static bool PointIsVisibleToCamera(Vector2 point)
        {
        if (Camera.main.WorldToViewportPoint(point).x + .3f < 0 || Camera.main.WorldToViewportPoint(point).x - .3f > 1 || Camera.main.WorldToViewportPoint(point).y - .3f > 1 || Camera.main.WorldToViewportPoint(point).y + .3f < 0)
            return false;
        return true;
        }
    }
