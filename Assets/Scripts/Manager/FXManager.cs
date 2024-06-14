using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : Singleton<FXManager>
{
    public List<AudioClip> fxs = new List<AudioClip>();

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFX(int index)
    {
        audioSource.PlayOneShot(fxs[index]);
    }
    
}
