using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class BrokenLight : MonoBehaviour
{
    new Light light;


    [SerializeField] float minMalfuctionDuration = 3f, maxMalfuctionDuration = 6f;
    [SerializeField] float minMalfunctionInterval = 10, maxMalfunctionInterval = 15;
    float lastMalfunctionTime = 0;
    float nextMalfunctionInterval;


    [Header("Trembling Effect Parameters")]
    [SerializeField] float tremblingValue = 0.3f;
    [SerializeField] float intervalTimeMax = 0.15f, intervalTimeMin = 0.05f;
    float lastTimeTremble;
    float intensity;
    float ogIntensity;
    [Space]
    [SerializeField] List<AudioClip> flickerSoundsList;
    bool playSound = false;
    private void Awake()
    {
        light = GetComponent<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        intensity = light.intensity;
        ogIntensity = intensity;
        nextMalfunctionInterval = 0;
        lastMalfunctionTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastMalfunctionTime >= nextMalfunctionInterval)
        {
            Debug.Log("TREMBLING");
            float duration = Random.Range(minMalfuctionDuration, maxMalfuctionDuration);
            StartCoroutine(DoTremblingEffect(duration));
            nextMalfunctionInterval = Random.Range(minMalfunctionInterval, maxMalfunctionInterval);
        }
        Debug.Log(nextMalfunctionInterval);
    }

    private IEnumerator DoTremblingEffect(float duration)
    {
        float timer = duration;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (Time.time - lastTimeTremble >= Random.Range(intervalTimeMin, intervalTimeMax))
            {
                PlaySound();
                var tempLightRange = intensity + Random.Range(-tremblingValue, tremblingValue);
                light.intensity = tempLightRange;
                lastTimeTremble = Time.time;
            }

            yield return null;
        }
        lastMalfunctionTime = Time.time;
        light.intensity = ogIntensity;
    }

    private void PlaySound()
    {
        playSound = !playSound;
        if (!playSound)
        {
            return;
        }
        int rnd = Random.Range(0, flickerSoundsList.Count - 1);
        AudioClip rndClip = flickerSoundsList[rnd];
        SoundManager.Get().TryPlaySound(rndClip);
    }
}
