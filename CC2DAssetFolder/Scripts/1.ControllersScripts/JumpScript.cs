using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{

    public string buttonAction;

    [Range(0, 100)]
    [Tooltip("Control how high this game object is able to jump")]
    public float jForce = 2.0f;

    [Tooltip("Check if you are on the ground or not")]
    public bool isGrounded = false;

    [Tooltip("The number of jumps you have until you have to get back on the ground")]
    [Range(0, 100)]
    public int numJumps;
    public int currJumps;
    public bool unlimitedJumps = false;

    [Tooltip("The amount of time gameobject has to jump after leaving the ground")]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Tooltip("Get the position of a transform to help check for if grounded")]
    public Transform groundCheck;
    public ContactFilter2D jumpfilter;
    Collider2D[] results = new Collider2D[2];
    [Tooltip("Get width where its allowed to check for grounded")]
    public float groundCheckWidth = 1.0f;
    [Tooltip("Get height where its allowed to check for grounded")]
    public float groundCheckHeight = 1.0f;

    Rigidbody2D rb;

    HealthScript healthScript;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckInput())
        {
            if (Input.GetKeyDown(buttonAction) && (coyoteTimeCounter > 0f || currJumps > 0 || unlimitedJumps))
            {
                if (coyoteTimeCounter < 0f)
                {
                    currJumps--;
                }
                coyoteTimeCounter = 0f;
                isGrounded = false;
                Debug.Log("jump");
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector3.up * jForce, ForceMode2D.Impulse);

            }
        }

       if(/*!(PAS._PlayerControl.Player.Jump.ReadValue<float>() > 0) &&*/ rb.velocity.y > 0f)
       {
         coyoteTimeCounter = 0f;
       }



       isGrounded = (Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, jumpfilter, results) > 1);

       if(isGrounded)
       {
            currJumps = numJumps;
         coyoteTimeCounter = coyoteTime;
         
       }
       else
       {
         coyoteTimeCounter -= Time.deltaTime;
       }
    }

    public bool CheckInput()
    {
        
        if (healthScript)
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheck.position, new Vector3(groundCheckWidth, groundCheckHeight, 5f));
    }
}
