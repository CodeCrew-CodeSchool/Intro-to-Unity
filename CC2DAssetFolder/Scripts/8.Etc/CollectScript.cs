using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    [Range(0, 1000)]
    public int scoreToAdd=10;

    [Range(0f, 5f)]
    public float range= 0.5f;
    [Tooltip("Filter any object by layer to deal damage to")]
    public LayerMask enefilter;
    public bool showAnim=false;

    

    private bool canbeCollect = true;
    private GoalManagerScript goalManager;
    private AnimationScript animScript;
    private ObjectSoundScript objSound;



    [Header("Debug")]
    public bool on = true;
    public Color wireFrameColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        goalManager = GameObject.FindObjectOfType<GoalManagerScript>();
        animScript = GetComponent<AnimationScript>();
        objSound = GetComponent<ObjectSoundScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, range, enefilter);

        for (int i = 0; i < coll.Length; i++)
        {
            if (canbeCollect)
            {
                if (objSound)
                {
                    objSound.PlayDeathSound();
                }
                if (animScript)
                {
                    animScript.dead = true;
                }
                if(goalManager)
                {
                    goalManager.AddScore(scoreToAdd);
                }
                canbeCollect = false;
                if(showAnim)
                {
                    StartCoroutine(destroyIt());
                }
                else
                {
                    Destroy(gameObject);
                }
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

    IEnumerator destroyIt()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
