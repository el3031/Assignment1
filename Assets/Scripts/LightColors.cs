using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColors : MonoBehaviour
{
    private Light light;
    [SerializeField] private Color[] colors;
    int current = 0;
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(SwitchColors());
    }

    // this is literally just for fun
    //change colors every second
    IEnumerator SwitchColors()
    {
        if (current == colors.Length)
        {
            current = 0;
        }
        light.color = colors[current++];

        float timer = 1f;
        float elapsed = 0f;
        while (elapsed < timer)
        {
            yield return null;
            elapsed += Time.deltaTime;
        }
        StartCoroutine(SwitchColors());
    }
}
