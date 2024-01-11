using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundScript : MonoBehaviour
{
    public AudioClip attackSound;
    public AudioClip shootSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public GameObject soundMaker;

    public void PlayFireSound()
    {
        PlayTheSound(shootSound);
    }

    public void PlayHurtSound()
    {
        PlayTheSound(hurtSound);
    }

    public void PlayDeathSound()
    {
        PlayTheSound(deathSound);
    }

    public void PlayMeleeSound()
    {
        PlayTheSound(attackSound);
    }

    public void PlayTheSound(AudioClip a)
    {
        if (CheckForSound(a))
        {
            GameObject SM = Instantiate(soundMaker, transform.position, Quaternion.identity);
            AudioSource source = SM.GetComponent<AudioSource>();
            source.clip = a;
            source.Play();
        }
    }

    public bool CheckForSound(AudioClip a)
    {
        if (a == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
