using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSound : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    [SerializeField] float soundGenerationDelay = 0.0f;
    [SerializeField] bool isLoop = false;

    private void Awake()
    {
        if(soundGenerationDelay <= 0f) { SoundManager.Get().TryPlaySound(clip, isLoop); }
        else { StartCoroutine(SoundDelay()); }
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(soundGenerationDelay);
        SoundManager.Get().TryPlaySound(clip, isLoop);
    }
}
