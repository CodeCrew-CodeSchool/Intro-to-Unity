using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AI_Shooter : AI
{

    public GameObject projectile;
    public Transform shootPositionFlipped;
    public Transform shootPositionNotFlipped;

    public float transdist;
    public float dist;
    public Transform target;
    
    public bool goingRight = true;
    public bool turnAble = true;
    private bool turning = true;

    private bool stop = false;

    public bool ignoreParameters;
    public float bulletForce;
    public bool permanentForce = true;
    [Range(0, 100)]
    public float bulletDestroyTime = 1f;
    [Range(0, 100)]
    public float speed = 1.0f;

   

    

    protected override void behaviour()
    {
        base.behaviour();
        dist = Vector2.Distance(transform.position, target.position);
        
        if(dist < transdist)
        {
            currState = state.attacker;
        }

        switch(currState)
        {
            case state.idle:
                if (CheckInput())
                {
                    if(CheckAnimation())
                    {
                        AnimScript.moving = false;
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
                break;
            case state.protrolling:

                break;
            case state.attacker:
                if(CheckInput())
                {
                    if(CheckAnimation())
                    {
                        AnimScript.moving = false;
                    }
                    AttackerMode();
                }
                
                break;
        }
    }

    public void AttackerMode()
    {
        if(turnAble)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if(target.position.x > transform.position.x && turning)
            {
                spriteRenderer.flipX = false;
            }
            else if(target.position.x < transform.position.x && turning)
            {
                spriteRenderer.flipX = true;
            }
        }

        if(!stop)
        {
            stop = true;
            StartCoroutine(readyToFire());
        }
    }

    IEnumerator readyToFire()
    {
        
        yield return new WaitForSeconds(1f);
        turning = false;

        yield return new WaitForSeconds(1.5f);
        if (CheckSound())
        {
            soundScript.PlayFireSound();
        }
        if(CheckInput())
        {
            fire();
        }
        

        yield return new WaitForSeconds(1.0f);
        stop = false;
        turning = true;
    }
    
    public void ChangeDirection()
    {
        if (goingRight)
        {
            goingRight = false;
        }
        else
        {
            goingRight = true;
        }
    }

    public void fire()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Transform pos;
        float Force = bulletForce;
        if (sr.flipX)
        {
            pos = shootPositionFlipped;
        }
        else
        {
            pos = shootPositionNotFlipped;
        }

        GameObject bullet;
        if (ignoreParameters)
        {
            Instantiate(projectile, pos.position, pos.rotation);
        }
        else
        {
            bullet = Instantiate(projectile, pos.position, pos.rotation);

            bullet.GetComponent<CC2DSideScrollerBullet>().permanentForce = permanentForce;
            


            if (permanentForce)
            {
                bullet.GetComponent<CC2DSideScrollerBullet>().bulletForce = Force;
            }
            else
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * Force);
            }

            bullet.GetComponent<CC2DSideScrollerBullet>().destroyTime = bulletDestroyTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            Debug.Log("go");
            ChangeDirection();
        }
    }
}
