using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectionButtonHandler : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroupRuneWindow;
    [SerializeField] private CanvasGroup canvasGroupStatsWindow;
    [SerializeField] private CanvasGroup canvasGroupCollection;

    public void ReturnToMenu()
    {
        ToggleCanvasGroup(canvasGroupCollection);
    }

    public void ToggleWindows()
    {
        ToggleCanvasGroup(canvasGroupStatsWindow);
        ToggleCanvasGroup(canvasGroupRuneWindow);
    }

    public void ToggleCanvasGroup(CanvasGroup canvasGroup)
    {
        if(canvasGroup.alpha == 1)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
