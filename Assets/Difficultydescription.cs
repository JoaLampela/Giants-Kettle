using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Difficultydescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] string easyModeDescription;
    [SerializeField] string normalModeDescription;
    [SerializeField] string hardModeDescription;
    [SerializeField] string lunaticModeDescription;
    [SerializeField] private CanvasGroup canvas;

    public void DisplayLunaticDescription()
    {
        canvas.alpha = 1;
        text.text = lunaticModeDescription;
    }
    public void HardDescription()
    {
        canvas.alpha = 1;
        text.text = hardModeDescription;
    }
    public void NormalDescription()
    {
        canvas.alpha = 1;
        text.text = normalModeDescription;
    }
    public void EasyDescription()
    {
        canvas.alpha = 1;
        text.text = easyModeDescription;
    }
    public void Hide()
    {
        canvas.alpha = 0;
    }
}
