using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAttackScript : MonoBehaviour
{
    
    [Range(1, 100)]
    [Tooltip("Deal damage to any object that touch it")]
    public float damage;
    [Tooltip("Filter any object by layer to deal damage to")]
    public LayerMask enefilter;
    [Range(0.01f, 100f)]

    public float range = 0.05f;

    private HealthScript healthScript;

    [Header("Debug")]
    public bool on = true;
    public Color wireFrameColor = Color.red;


    // Start is called before the first frame update
    void Start()
    {
        if( TryGetComponent<HealthScript>(out HealthScript hS))
        {
            healthScript = GetComponent<HealthScript>();
        }

         
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript == null)
        {
            ContactAttack();
        }
        else
        {
            if(!healthScript.dead)
            {
                ContactAttack();
            }
        }

    }

    public void ContactAttack()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, range, enefilter);

        for(int i = 0; i < coll.Length; i++)
        {
            
            if (coll[i].GetComponent<HealthScript>() != null)
            {
                Debug.Log(coll[i].name);
                coll[i].GetComponent<HealthScript>().setDirection(transform);
                coll[i].GetComponent<HealthScript>().damage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (on)
        {
            Gizmos.color = wireFrameColor;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
