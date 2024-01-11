using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{

    public GameObject projectile;

    public string shootKey;

    public Transform shootPositionNotFlipped;
    public Transform shootPositionFlipped;

    public float shootTime;
    private float shootTimeCounter;

    public bool flippable = true;

    public bool ReadyToFire = true;

    public bool ignoreParameters;
    

    public float bulletForce;
    public bool permanentForce = true;
    public float bulletDestroyTime = 1f;

    private ObjectSoundScript sound;
    private AnimationScript animSc;

   // private bool inputAble = true;

    private HealthScript healthScript;
    

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
        animSc = GetComponent<AnimationScript>();
        sound = GetComponent<ObjectSoundScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(shootKey) && ReadyToFire)
        {
            if (CheckInput())
            {
                fireBullet();
            }
        }

    }

    public void fireBullet()
    {
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Transform pos;

        float Force = bulletForce;

        if(animSc != null)
        { 
            animSc.shoot = true;
        }

        if (flippable)
        {
            if (sr.flipX)
            {
                pos = shootPositionFlipped;
            }
            else
            {
                pos = shootPositionNotFlipped;
            }
        }
        else
        {
            pos = shootPositionNotFlipped;
        }

        GameObject bullet;
        if(ignoreParameters)
        {
            Instantiate(projectile, pos.position, pos.rotation);
        }
        else
        {
            bullet = Instantiate(projectile, pos.position, pos.rotation);

            bullet.GetComponent<CC2DSideScrollerBullet>().permanentForce = permanentForce;
            


            if (permanentForce)
            {
                bullet.GetComponent<CC2DSideScrollerBullet>().bulletForce = Force;
            }
            else
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * Force);
            }

            bullet.GetComponent<CC2DSideScrollerBullet>().destroyTime = bulletDestroyTime;

            //.GetComponent<CC2DSideScrollerBullet>().bulletBehaviour();


        }

        if(sound != null)
        {
            sound.PlayFireSound();
        }
        
        StartCoroutine(ResetShooting());
        //shootTimeCounter = shootTime;
    }

    public bool CheckInput()
    {

        if (healthScript != null)
        {
            if(healthScript.dead)
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

    IEnumerator resetAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        animSc.shoot = false;
    }
    
    IEnumerator ResetShooting()
    { 
        ReadyToFire = false;
        yield return new WaitForSeconds(shootTime);
        ReadyToFire = true;
    }
}
