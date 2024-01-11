/*With this script you can use your mouse to rotate 
 a game object*/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateToMouseScript : MonoBehaviour
{
    //Hold the mouse position
    Vector2 mousePos;
    //Hold the camera
    [Tooltip("This get the camera to help where that rotation be calculated")]
    public Camera cam;

    [Tooltip("Allow for the user to left click to shoot" +
        " Shooting Script is needed though")]
    public bool clickToShoot = false;

    [Tooltip("Get the shooting script to allow for shooting")]
    public ShootingScript shootScript;

    //Get this hold Rigidbody that will be needed
    private Rigidbody2D rb;

    HealthScript healthScript;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (clickToShoot)
        {
            if(Input.GetMouseButton(0))
            {
                if(shootScript.ReadyToFire)
                {
                    shootScript.fireBullet();
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (CheckInput())
        {
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public bool CheckInput()
    {
        if (healthScript != null)
        {
            if (healthScript.dead)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }
}
