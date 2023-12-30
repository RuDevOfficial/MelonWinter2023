using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLight : MonoBehaviour
{
    Light light;

    [Header("Light Effect Parameters")]
    [SerializeField] float tremblingValue = 0.3f;
    [SerializeField] float intervalTimeMax = 0.15f, intervalTimeMin = 0.05f;
    float lastTimeTremble;
    float range;

    private void Awake()
    {
        light = GetComponent<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        range = light.range;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTremblingEffect();
    }

    private void UpdateTremblingEffect()
    {
        if (Time.time - lastTimeTremble >= Random.Range(intervalTimeMin, intervalTimeMax))
        {
            var tempLightRange = range + Random.Range(-tremblingValue, tremblingValue);
            light.intensity = tempLightRange;
            lastTimeTremble = Time.time;

            //if (light.intensity <= 0)
            //{
            //    light.intensity = 0.3f;
            //}
        }

    }
}
