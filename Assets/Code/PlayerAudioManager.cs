using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public BoatController boatController;
    public AudioSource lwSource;
    public AudioSource rwSource;

    [SerializeField] private bool lwIsCurrentlyFading;
    [SerializeField] private bool rwIsCurrentlyFading;

    private void Start()
    {
        lwIsCurrentlyFading = false;
        rwIsCurrentlyFading = false;
    }


    void Update()
    {
        PlayAudio();
        StopAudio();
    }

    private void PlayAudio()
    {
        if (boatController.leftWheelSpeed != 0 && !lwSource.isPlaying)
        {
            StartCoroutine (AudioFadeScript.FadeIn(lwSource, 0.5f, 1));
        }
        if (boatController.rightWheelSpeed != 0 && !rwSource.isPlaying)
        {
            StartCoroutine (AudioFadeScript.FadeIn(rwSource, 0.5f, 1));
        }
    }
    private void StopAudio()
    {
        if (boatController.leftWheelSpeed == 0 && lwSource.isPlaying && !lwIsCurrentlyFading)
        {
            StartCoroutine(ShouldFade(lwIsCurrentlyFading));
        }
        if (boatController.rightWheelSpeed == 0 && rwSource.isPlaying && !rwIsCurrentlyFading)
        {
            StartCoroutine(ShouldFade(rwIsCurrentlyFading));
        }
    }

    private IEnumerator ShouldFade(bool wheel)
    {   
        yield return new WaitForSeconds(0.5f);
        
        if (boatController.leftWheelSpeed == 0 && lwSource.isPlaying && !lwIsCurrentlyFading)
        {
            lwIsCurrentlyFading = true;
            StartCoroutine (AudioFadeScript.FadeOut (lwSource, 0.5f));
            yield return new WaitForSeconds(0.75f); 
            lwIsCurrentlyFading = false;
        }
        if (boatController.rightWheelSpeed == 0 && rwSource.isPlaying && !rwIsCurrentlyFading)
        {
            rwIsCurrentlyFading = true;
            StartCoroutine (AudioFadeScript.FadeOut (rwSource, 0.5f));
            yield return new WaitForSeconds(0.75f);
            rwIsCurrentlyFading = false;
        }
    }
}
