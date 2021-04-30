using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunePointTextEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI effectTect;

    private float fontExpandAmount = 2;
    private float effectDuration = 1f;
    private float currentTime = 0;

    private float startFontSize;
    private bool goingUp = true;
    // Update is called once per frame

    private void Start()
    {
        startFontSize = effectTect.fontSize;
    }
    void Update()
    {
        if(goingUp)
        {
            if (currentTime < effectDuration)
            {
                currentTime += Time.deltaTime;
                effectTect.fontSize += (fontExpandAmount / effectDuration) * Time.deltaTime - (fontExpandAmount / effectDuration) * Time.deltaTime * currentTime;
            }
            else
            {
                currentTime = 0;
                goingUp = false;
            }
        }
        else
        {
            if (currentTime < effectDuration)
            {
                currentTime += Time.deltaTime;
                effectTect.fontSize -= (fontExpandAmount / effectDuration) * Time.deltaTime - (fontExpandAmount / effectDuration) * Time.deltaTime * currentTime;
            }
            else
            {
                currentTime = 0;
                goingUp = true;
            }
        }

    }
}
