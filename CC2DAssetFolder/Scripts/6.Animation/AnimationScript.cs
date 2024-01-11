using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    //public CC2DSideScrollerScript side;
    
    public Animator anim;
    public bool moving = false;
    public bool shoot = false;
    public bool melee = false;
    public bool dead = false;
    public bool hurt = false;
    

    public bool noChange = false;
    private AnimationClip clip;
    // Start is called before the first frame update
    void Start()
    {
       
        anim = GetComponent<Animator>();
        //side = GetComponent<CC2DSideScrollerScript>()  
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
        {
            hurt = false;
        }
        //a.clip = meleeAnim;
        if (!dead)
        {
            if(melee)
            {
                anim.Play("Melee_anim");
                clip = anim.runtimeAnimatorController.animationClips[3];
                Debug.Log(clip.name);
                StartCoroutine(animDone(shoot, clip));

            }
            else if(shoot)
            {
                //noChange = true;

                anim.Play("Fire_anim");
                if(anim.runtimeAnimatorController.animationClips[2])
                {
                    clip = anim.runtimeAnimatorController.animationClips[2];
                }
                Debug.Log(clip.name);
                StartCoroutine(animDone(shoot, clip));
            }
            else if (moving)
            {
                anim.Play("Walk_Anim");
            }
            else
            {
                anim.Play("Idle_Anim");
            }
        }
        else if(hurt)
        {
            anim.Play("Hurt_anim");
            clip = anim.runtimeAnimatorController.animationClips[5];
            Debug.Log(clip.name);
            StartCoroutine(animDone(shoot, clip));
        }
        else if(dead)
        {
            anim.Play("Dead_anim");
        }
    }

    IEnumerator animDone(bool done, AnimationClip a)
    {
        if(a)
        {
            yield return new WaitForSeconds(a.length);
        }
        else
        {
            yield return new WaitForSeconds(0f);
        }
        
        if (shoot)
        {
            shoot = false;
        }
        if(melee)
        {
            melee = false;   
        }
        if(hurt&& !dead)
        {
            hurt = false;
        }
    }
}
