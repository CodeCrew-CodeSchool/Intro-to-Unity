using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreollisionScript : MonoBehaviour
{
    public bool ignoreCertainCollision = true;
    public int[] layerToIgnore;

    // Start is called before the first frame update
    void Start()
    {
        StopCollisions();
    }

    // Update is called once per frame
    void Update()
    {
        StopCollisions();
    }

    public void StopCollisions()
    {
        if (ignoreCertainCollision)
        {
            int numL = GetThisLayer();
            for (int i = 0; i < layerToIgnore.Length; i++)
            {
                Physics2D.IgnoreLayerCollision(numL, layerToIgnore[i], true);
            }
        }
    }

    public int GetThisLayer()
    {
        return gameObject.layer;
    }
}
