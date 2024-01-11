using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

public class AI_Chasing : AI
{
    public enum chaseType {goRight, goLeft, goBoth}
    public chaseType type = chaseType.goRight;

    public float speed = 1.0f;

    public float transdist = 1.0f;
    public float dist;

    public Transform target;

    public float turnTime = 1.0f;

    private bool stop = false;
    private bool ahead = true;
    private bool dontChange = false;


    protected override void behaviour()
    {
        dist = Vector2.Distance(transform.position, target.position);

        if(dist < transdist)
        {
            currState = state.attacker;
        }

        if (CheckInput())
        {
            switch (currState)
            {
                case state.idle:
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    break;
                case state.attacker:

                    if (CheckAnimation())
                    {
                        AnimScript.moving = true;
                    }
                    action();

                    break;
            }
        }
        else
        {
            if (CheckAnimation())
            {
                AnimScript.moving = false;
            }
            
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void action()
    {
        switch(type)
        {
            case chaseType.goRight:
                rb.velocity = new Vector2(speed, rb.velocity.y);
            break; 
            case chaseType.goLeft:
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            break; 
            case chaseType.goBoth:
                IfBoth();
             break;
        }
    }

    public void IfBoth()
    {
        //bool ahead;
        if (!stop)
        {
            ahead = transform.position.x < target.position.x;
            stop = true;
        }

        if(ahead)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if (!dontChange)
        {
            if (ahead && transform.position.x < target.position.x)
            {
                StartCoroutine(seeIfChange(ahead));
            }
            else if (!ahead && transform.position.x > target.position.x)
            {
                StartCoroutine(seeIfChange(ahead));
            }
        }
    }

    IEnumerator seeIfChange(bool a)
    {
        dontChange = true;
        yield return new WaitForSeconds(turnTime);
        dontChange  = false;
        ahead = transform.position.x < target.position.x;

    }

    
}
