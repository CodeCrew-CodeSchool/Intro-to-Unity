using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CC2DSideScrollerBullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public LayerMask filter;
    public float bulletForce;

    public Vector2 hitBoxSize;

    public bool permanentForce = true;
    private bool stop = true;

    public bool ignoreParameters = false;
    public float damage;

    public float destroyTime = 1.0f;
    public bool destroyOnContact = true;

    [Header("Debug")]
    public bool on = true;
    public Color debugColor= Color.red;



    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletBehaviour();
    }

    public void bulletBehaviour()
    {

        Collider2D[] coll = Physics2D.OverlapBoxAll(transform.position, hitBoxSize, 0.0f, filter, -5.0f, 5.0f);
        
        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].TryGetComponent<HealthScript>(out HealthScript hs))
            {
                coll[i].GetComponent<HealthScript>().damage(damage);
                if(destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
        }

        StartCoroutine(dTime());
        if (!ignoreParameters)
        {
            
            if (permanentForce)
            {
                rb.velocity = transform.up * bulletForce;
            }
            else if (!permanentForce && stop == true)
            {
                stop = false;
                rb.velocity = transform.up * bulletForce;
            }
        }
        else
        {
            if (permanentForce)
            {
                Debug.Log("Yes");
                rb.velocity = transform.up * bulletForce;
            }
        }
    }

    public IEnumerator dTime()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (destroyOnContact)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (destroyOnContact)
        {
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (on)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawWireCube(transform.position, hitBoxSize);
        }
    }



}
