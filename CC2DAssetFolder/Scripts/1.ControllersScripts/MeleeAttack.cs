using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    //public ButtonsWorks.buttons meleeButton = ButtonsWorks.buttons.c;
    public string meleeKey;

    [Range(0, 100)]
    public float meleeTime = 1.0f;
    private float meleeTimeCounter;
    public bool ready = true; 

    public Transform meleePositionFlipped;
    public Transform meleePositionNotFlipped;
    public Vector2 hitBoxSize;

    public LayerMask filter;
    [Range(0, 100)]
    public float damage=1.0f;

    private AnimationScript script;
    private HealthScript healthScript;

    public bool on = true;
    public Color debugColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
        script = GetComponent<AnimationScript>();
        JustInCase();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(meleeKey))
        {
            if(CheckInput())
            {
                MeleeMove();
            }
        }
    }

    public void MeleeMove()
    {
        if (ready)
        {
            if(script!= null)
            {
                script.melee = true;
            }
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Transform theTarget;

            if (sr.flipX)
            {
                theTarget = meleePositionNotFlipped;
            }
            else
            {
                theTarget = meleePositionFlipped;
            }

            Collider2D[] coll = Physics2D.OverlapBoxAll(theTarget.position, hitBoxSize, 0.0f, filter, -5.0f, 5.0f);
        
             for (int i = 0; i < coll.Length; i++)
                {
                Debug.Log(coll[i].gameObject.name);
                if (coll[i].TryGetComponent<HealthScript>(out HealthScript hs))
                {

                    coll[i].GetComponent<HealthScript>().damage(damage);
                }
                }
            StartCoroutine(ResetMelee());
        }
    }

    IEnumerator ResetMelee()
    {
        ready = false;
        yield return new WaitForSeconds(meleeTime);
        ready = true;
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

    public void SetMeleeReady(bool TrueOrFalse)
    {
        ready = TrueOrFalse;
    }

    public void JustInCase()
    {
        if(meleeKey == "") 
        {
            meleeKey = "c";
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (on)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawWireCube(meleePositionFlipped.position, hitBoxSize);
            Gizmos.DrawWireCube(meleePositionNotFlipped.position, hitBoxSize);
        }
    }
}
