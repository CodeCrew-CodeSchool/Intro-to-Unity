using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AI_goToObject : AI
{


    [Range(0, 100)]
    public float spd = 1.0f;
    public enum useTarget {useGameObject, useTags, useName};
    public useTarget targetSettings = useTarget.useGameObject;
    public string targetTagOrName = "Player";

    public Transform target;

    public bool RotateTowardsPosition = false;

    public bool OnAttackNoMatter = false;
    public bool ableToGoBackToIdle = false;

    [Range(1, 100)]
    public float Attack_Dist = 10.0f;
    public float dist;
    // Start is called before the first frame update

    protected override void StartFunction()
    {
        base.StartFunction();
        GetTarget();
    }

    protected override void behaviour()
    {
        base.behaviour();

        dist = Vector2.Distance(transform.position, target.position);

        if(OnAttackNoMatter)
        {
            currState = state.attacker;
        }
        else
        {
            if (dist < Attack_Dist)
            {
                currState = state.attacker;
            }
            else if ((dist > Attack_Dist))
            {
                if (ableToGoBackToIdle)
                {
                    currState = state.idle;
                }
            }
        }
        

        Vector3 dir = (target.position - transform.position).normalized;

        if(currState == state.attacker)
        {
            if(CheckInput())
            {
                if(CheckAnimation())
                {
                   AnimScript.moving = true;
                }
                if (RotateTowardsPosition)
                {
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                }

                 rb.velocity = dir * spd;
               
            }
        } 
        else
        {
            if (CheckAnimation())
            {
                AnimScript.moving = false;
            }
            rb.velocity = Vector3.zero;
        }

        if(currState == state.idle)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void GetTarget()
    {
        switch(targetSettings)
        {
            case useTarget.useGameObject:
                
                break;
            case useTarget.useTags:
                target = GameObject.FindWithTag(targetTagOrName).transform;
                break;
            case useTarget.useName:
                target = GameObject.Find(targetTagOrName).transform;
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
      
    }
}
