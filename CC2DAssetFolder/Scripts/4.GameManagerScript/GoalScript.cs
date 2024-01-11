using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public GoalManagerScript _manager;

    public LayerMask filter;
    public float range = 1f;

    [Header("Debug Setting")]
    public bool on = true;
    public Color debugColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindObjectOfType<GoalManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, range, filter);

        for (int i = 0; i < coll.Length; i++)
        {
            if (_manager != null)
            {
                _manager.FinishLevel();
            }
                
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (on)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }





}
