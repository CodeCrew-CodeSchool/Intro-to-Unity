using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC2DSideScrollerScript : MonoBehaviour
{

    public string LeftButton;
    public string RightButton;
    public Rigidbody2D rb;

    public bool equalizeSpeed = true;
    public float rSpeed = 1f;
    public float lSpeed = 1f;
    
    public bool noMovementInAir;

    public bool InputAble = true;

    private SpriteRenderer sr;
    public bool ableFlip = true;

    public bool moving = false;

    private AnimationScript animSc;
    private HealthScript healthScript;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
        animSc = GetComponent<AnimationScript>();
        justInCase();

        rb = GetComponent<Rigidbody2D>();  

        sr = GetComponent<SpriteRenderer>();

        if(equalizeSpeed) 
        {
            lSpeed = rSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript != null)
        {
            if (healthScript.dead)
            {
                InputAble = false;
            }
        }

        if(InputAble)
        {
            if (Input.GetKey(RightButton))
            {
                if (ableFlip)
                {
                    sr.flipX = false;
                }
                rb.velocity = new Vector2(rSpeed, rb.velocity.y);
                if (animSc != null)
                {
                    animSc.moving = true;
                }
            }
            else if (Input.GetKey(LeftButton))
            {
                if (ableFlip)
                {
                    sr.flipX = true;
                }
                rb.velocity = new Vector2(-lSpeed, rb.velocity.y);
                if (animSc)
                {
                    animSc.moving = true;
                }
            }
            else
            {
                if (animSc)
                {
                    animSc.moving = false;
                }
            }
        }

    }

    //If the user never input anything for left or Right
    public void justInCase()
    {
        if(LeftButton == "")
        {
            LeftButton = "left";
        }

        if(RightButton == "")
        {
            RightButton = "right";
        }
    }

    
}
