using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public bool turnedOn = true;

    Light2D light;

    [Header("Big flicker settings")]
    [SerializeField]
    float bigFlickerWaveIntervalNormal = 2;
    [SerializeField]
    float bigFlickerWaveIntervalRandom = 2;

    [SerializeField]
    float bigFlickerNormal = 0.1f;
    [SerializeField]
    float bigFlickerRandom = 0.05f;

    [SerializeField]
    float bigFlickerMinIntensity = 1;
    [SerializeField]
    float bigFlickerMaxIntensity = 1.2f;

    [SerializeField]
    float normalIntensity = 1;

    [SerializeField]
    int bigFlickerCountMin = 0;
    [SerializeField]
    int bigFlickerCountMax = 5;

    private void OnValidate()
    {
        GetComponent<Light2D>().intensity = normalIntensity;
    }

    private void Awake()
    {
        light = GetComponent<Light2D>();
    }
    private void Start()
    {
        StartCoroutine(Flicker());
    }
    IEnumerator BigFlicker()
    {
        for (int i = 0; i < Random.Range(bigFlickerCountMin,bigFlickerCountMax); i++)
        {
            light.intensity = Random.Range(bigFlickerMinIntensity,bigFlickerMaxIntensity);
            yield return new WaitForSeconds(bigFlickerNormal+Random.Range(0,bigFlickerRandom));
        }
        light.intensity = normalIntensity;
    }

    IEnumerator Flicker()
    {
        while(turnedOn)
        {
            yield return new WaitForSeconds(bigFlickerWaveIntervalNormal+Random.Range(0, bigFlickerWaveIntervalRandom));
            StartCoroutine(BigFlicker());
        }
    }
}
