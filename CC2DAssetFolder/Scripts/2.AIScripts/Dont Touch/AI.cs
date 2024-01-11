using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public enum state { idle, protrolling, attacker};
    public state currState = state.idle;
    protected HealthScript healthScript;
    protected ObjectSoundScript soundScript;
    protected AnimationScript AnimScript;
    protected SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartFunction();
        soundScript = GetComponent<ObjectSoundScript>(); 
        healthScript = GetComponent<HealthScript>();
        AnimScript =  GetComponent<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Yes");
        behaviour();
    }

    protected virtual void StartFunction()
    {

    }

    protected virtual void behaviour()
    {
        
    }

    public bool CheckSound()
    {
        if(soundScript == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CheckAnimation()
    {
        if(AnimScript)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckInput()
    {

        if (healthScript!=null)
        {
            if (healthScript.dead || healthScript.noInputCurrently)
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
