using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightOverTimeChange : MonoBehaviour
{
    
    private Color colorStart;
    private Color currentColor;
    private float startSize = 0;
    private float currentSize;
    private bool decreaseIntensity = false;


    [Header("Color Settings")]
    public Light2D thislight;
    [SerializeField] private Color colorEnd;
    public bool pulsingColor;
    [SerializeField] [Range(0f, 5f)] float falloutDelay;
    [SerializeField] [Range(0f, 20f)] float falloutPerSecond;
    [SerializeField] [Range(0f, 5f)] float lerpTimeColor;
    
    [Header("Size Settings")]
    public bool pulsingSize;
    [SerializeField] [Range(0f, 20f)] float lerpTimeSize;
    [SerializeField] [Range(0f, 20f)] float newSize;

    private float tColor = 0f;
    private float tSize = 0f;

    void Start()
    {
        colorStart = thislight.color;
        currentColor = colorStart;
        startSize = thislight.pointLightOuterRadius;
        currentSize = startSize;
        StartCoroutine(FadeLight());
    }

    void Update()
    {
        ChangeColor();
        if(decreaseIntensity && thislight.intensity > 0)
        {
            thislight.intensity -= falloutPerSecond * Time.deltaTime;
            if (thislight.intensity < 0) thislight.intensity = 0;
        }
    }

    IEnumerator FadeLight()
    {
        yield return new WaitForSeconds(falloutDelay);
        decreaseIntensity = true;
        
    }

    // Update is called once per frame
    void ChangeColor()
    {
        
        currentColor = Color.Lerp(currentColor, colorEnd, lerpTimeColor * Time.deltaTime);
        thislight.color = currentColor;

        tColor = Mathf.Lerp(tColor, 1f, lerpTimeColor * Time.deltaTime);

        if(tColor > .9f)
        {
            if(pulsingColor)
            {
                Color temp = colorStart;
                colorStart = colorEnd;
                colorEnd = temp;
                tColor = 0;
            }
        }

        currentSize = Mathf.Lerp(currentSize, newSize, lerpTimeSize * Time.deltaTime);
        thislight.pointLightOuterRadius = currentSize;

        tSize = Mathf.Lerp(tSize, 1f, lerpTimeSize * Time.deltaTime);

        if(tSize > .9f)
        {
            if(pulsingSize)
            {
                float temp = startSize;
                startSize = newSize;
                newSize = temp;
                tSize = 0;
            }
        }

        
    }
}
