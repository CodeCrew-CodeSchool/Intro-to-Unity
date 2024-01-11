using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Health")]
    [Range(1, 100)]
    [Tooltip("The starting and the max health object can be")]
    public float maxHealth;
    [Tooltip("The current health of the object")]
    public float currHealth;
    [Tooltip("This check if the character is dead")]
    public bool dead = false;

    public float destroyTime = 0.0f;

    
    public enum deathReaction { disappear, restart};
    [Tooltip("See if it the object sould restart the game or just disappear")]
    public deathReaction reaction;

    [Header("Invinicibility")]
    public bool invinicibilityPossible = false;
    public bool invinicible = false;
    public float itime = 1.0f;

    //public ContactFilter2D enemyfilter;

    [Header("KnockBack")]
    public bool KBable;
    public enum KBTypes {SideScroller, TopDown};
    public KBTypes KBType;
    public Vector2 KBForce = new Vector2(0.0f, 0.0f);
    

    [Header("Stop Inputs")]
    public bool StopInputpossible = false;
    public float inputStopTime = 1.0f;
    public bool noInputCurrently = false;

    public HealthAddOn_Death deathSettings;

    //This is for topdown
    private Transform sender;

    private ObjectSoundScript sound;
    private AnimationScript AS;
    private GoalManagerScript goalScript;

    public int scoreToAdd = 0;

    // Start is called before the first frame update
    void Start()
    {
        goalScript = GetComponent<GoalManagerScript>();
        deathSettings = GetComponent<HealthAddOn_Death>();
        AS = GetComponent<AnimationScript>();
        sound = GetComponent<ObjectSoundScript>();
        currHealth = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(float damage)
    {
        
        if(!invinicible)
        {
            currHealth -= damage;
            Debug.Log(currHealth);
            knockBack();
            if ( currHealth <= 0)
            {

                currHealth = 0;
                dead = true;
                
            }
            AnimFunction();
            SoundFunction();
            StartCoroutine(invincibleTime());
            StartCoroutine(StopInputtingCoroutine());
        }

        if (dead)
        {
            StartCoroutine(DeathReactionNow());
        }
        

    }

    public void knockBack()
    {
        if (KBable)
        {
            switch (KBType)
            {
                case KBTypes.SideScroller:
                    GetComponent<Rigidbody2D>().AddForce(KBForce);
                    break;
                case KBTypes.TopDown: 
                   Vector2 direction = (transform.position - sender.position).normalized;
                    GetComponent<Rigidbody2D>().AddForce(direction * KBForce);
                    break;
            }
        }
    }

   public void DoOtherDeathAction()
    {
        if(deathSettings)
        {
            deathSettings.DeathReact();
        }
    }

    IEnumerator DeathReactionNow()
    {
        yield return new WaitForSeconds(destroyTime);
        if(reaction == deathReaction.disappear)
        {
            Destroy(gameObject);
        }
        else if(reaction == deathReaction.restart)
        {
            GameObject.FindObjectOfType<SceneManagerScript>().SceneOfDeath();
        }
    }

    public void gotoScore()
    {
        if(goalScript)
        {
            goalScript.AddScore(scoreToAdd);
        }
    }

    public void setDirection(Transform s)
    {
        sender = s;
    }
    
    public void SoundFunction()
    {
        if (sound != null)
        {
            if (dead)
            {
                sound.PlayDeathSound();
            }
            else
            {
                sound.PlayHurtSound();
            }
        }
    }

    public void AnimFunction()
    {
        if (AS != null)
        {
            if (dead)
            {
                AS.dead = true;
            }
            else
            {
                AS.hurt = true;
            }
        }
    }

    IEnumerator invincibleTime()
    {
        if(invinicibilityPossible)
        {
            invinicible = true;
            yield return new WaitForSeconds(itime);
            invinicible = false;
        }
       
    }

    public void healFunction(float h)
    {
        currHealth += h;

        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

    }
    
    

    IEnumerator StopInputtingCoroutine()
    {
        CC2DSideScrollerScript move2D = GetComponent<CC2DSideScrollerScript>();
        if (StopInputpossible)
        {
            noInputCurrently = true;
            yield return new WaitForSeconds(inputStopTime);
            noInputCurrently = false;
        }

    }

    public void InstantKill()
    {
        dead = true;
    }
    
    
}
