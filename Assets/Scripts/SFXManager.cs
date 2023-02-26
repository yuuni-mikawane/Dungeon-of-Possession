using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class SFXManager : SingletonBind<SFXManager>
{
    public AudioClip hurtFX;
    public AudioClip hitFX;
    public AudioClip dieFX;
    public AudioClip swallowFX;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHurtSFX()
    {
        audioSource.PlayOneShot(hurtFX);
    }

    public void PlayHitSFX()
    {
        audioSource.PlayOneShot(hitFX);
    }

    public void PlayDieSFX()
    {
        audioSource.PlayOneShot(dieFX);
    }
    public void PlaySwallowSFX()
    {
        audioSource.PlayOneShot(swallowFX);
    }
}
