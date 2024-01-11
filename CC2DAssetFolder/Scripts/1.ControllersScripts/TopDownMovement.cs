using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private SpriteRenderer sr;
    public string LeftButton;
    public string RightButton;
    public string UpButton;
    public string DownButton;

    public Rigidbody2D rb;
    public bool movementAble = true;
    public float vSpeed = 1.0f;
    public float hSpeed = 1.0f;

    public enum rotationStates {noRotation, Degrees_90, Degree_45, Smooth};
    public rotationStates rotationState;
    public float rotationspeed = 1.0f;
    
    HealthScript healthScript;
    public AnimationScript animScript;

    // Start is called before the first frame update
    void Start()
    {
        animScript = GetComponent<AnimationScript>();
        healthScript = GetComponent<HealthScript>();
        DefaultButtons();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(CheckInput())
        {
            animtionFunction();
            movement();
            rotationFunction();
        }
    }

    public void movement()
    {

            if (Input.GetKey(LeftButton))
            {
                rb.velocity = new Vector2(-hSpeed, rb.velocity.y);
            }
            else if (Input.GetKey(RightButton))
            {
                rb.velocity = new Vector2(hSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            if (Input.GetKey(UpButton))
            {
                rb.velocity = new Vector2(rb.velocity.x, vSpeed);
            }
            else if (Input.GetKey(DownButton))
            {
                rb.velocity = new Vector2(rb.velocity.x, -vSpeed);
            }
            else {

                rb.velocity = new Vector2(rb.velocity.x, 0);

            }
        

    }

    public void rotationFunction()
    {   
        if (rotationState != rotationStates.noRotation)
        {
            if (Input.GetKey(LeftButton))
            {
                if (rotationState == rotationStates.Smooth)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 90), rotationspeed);
                    
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }

            if (Input.GetKey(RightButton))
            {
                if (rotationState == rotationStates.Smooth)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 270), rotationspeed);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                }
            }

            if (Input.GetKey(UpButton))
            {
                if (rotationState == rotationStates.Smooth)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), rotationspeed);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (Input.GetKey(DownButton))
            {  if (rotationState == rotationStates.Smooth)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 180), rotationspeed);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }

            if(rotationState == rotationStates.Degree_45 || rotationState == rotationStates.Smooth)
            {
                if(Input.GetKey(RightButton) && Input.GetKey(UpButton))
                {
                    if(rotationState == rotationStates.Smooth)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 315), rotationspeed);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 315);
                    }
                }

                if (Input.GetKey(RightButton) && Input.GetKey(DownButton))
                {
                    if (rotationState == rotationStates.Smooth)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 225), rotationspeed);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 225);
                    }
                }

                if (Input.GetKey(LeftButton) && Input.GetKey(UpButton))
                {
                    if (rotationState == rotationStates.Smooth)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 45), rotationspeed);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                    }
                }

                if (Input.GetKey(LeftButton) && Input.GetKey(DownButton))
                {
                    if (rotationState == rotationStates.Smooth)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 135), rotationspeed);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 135);
                    }
                }

            }
        }
        
    }

    public void DefaultButtons()
    {
        if (LeftButton == "")
        {
            LeftButton = "left";
        }

        if (RightButton == "")
        {
            RightButton = "right";
        }

        if (UpButton == "")
        {
            UpButton = "up";
        }

        if (DownButton == "")
        {
            DownButton = "down";
        }


    }
    public bool CheckInput()
    {

        if (healthScript != null)
        {
            if (healthScript.dead|| healthScript.noInputCurrently)
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

    public void animtionFunction()
    {
        if(animScript)
        {
            if(Input.GetKey(LeftButton)|| Input.GetKey(DownButton)|| Input.GetKey(RightButton)||Input.GetKey(UpButton))
            {
                animScript.moving = true;
            }
            else
            {
                animScript.moving = false;
            }
        }
    }
}
