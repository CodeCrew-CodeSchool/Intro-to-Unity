using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_BackandForth : AI
{
   
    [Range(0,100)]
    public float speed = 1.0f;
    public bool goingRight = true;
    //private SpriteRenderer SR;

    protected override void behaviour()
    {
        base.behaviour();
        if (CheckInput())
        {
            if (CheckAnimation())
            {
                AnimScript.moving = true;
            }
                if (goingRight)
                {
                    
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
        } 
        else
        {
            if (CheckAnimation())
            {
                AnimScript.moving = false;
            }
        }
    }

    public void ChangeDirection()
    {
        if(goingRight)
        {
            SR.flipX = true;
            goingRight = false;
        }
        else
        {
            SR.flipX = false;
            goingRight = true;
        }
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trigger")
        {
            
            ChangeDirection();
        }
    }
}
