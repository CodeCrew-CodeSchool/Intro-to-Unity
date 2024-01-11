using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class HealthAddOn_Death : MonoBehaviour
{
    [Header("LauchAway")]
    public bool launch = false;
    public Vector2 launchForce = new Vector2(1.0f, 1.0f);
    

    [Header("Lose Contraints On Death")]
    public bool fall = false;
    public bool rotate = false;

    [Header("Ignore Collision on Death")]
    public bool ignoreCertainCollision = false;
    public int[] layerToIgnore;
    

    

    public void DeathReact()
    {
        StopCollisions();
        Launch();
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

    public void Launch()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if(rb)
        {
            rb.velocity = Vector2.zero;
            rb.velocity = launchForce;
            
            if (rotate)
            {
                rb.freezeRotation = false;
            }

            if (fall)
            {
                rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
                rb.gravityScale = 1;
            }
        }

        

    }

    public int GetThisLayer()
    {
        return gameObject.layer;
    }
}
