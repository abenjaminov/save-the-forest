using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public void PlayShiftEffect()
    {
        var audioSource = GetComponentInChildren<AudioSource>();

        if (audioSource != null)
        {
            audioSource.timeSamples = 1;
            audioSource.Play();
        }
    }
}
