using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobPanelScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public CanvasGroup startItemTip;
    public CanvasGroup inventoryTip;
    public CanvasGroup runeSelectionTip;
    public CanvasGroup inventoryInsideTip;
    public CanvasGroup hotBarTip;
    public CanvasGroup runePointTip;
    private bool tipsOn = true;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (PlayerPrefs.GetInt("TipsOn", 1) == 0) 
        {
            tipsOn = false;
            TogglePanelCanvas(canvasGroup);
            TogglePanelCanvas(inventoryInsideTip);
            TogglePanelCanvas(hotBarTip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if (tipsOn) {
                PlayerPrefs.SetInt("TipsOn", 0);
                tipsOn = false;
            }
            else {
                tipsOn = true;
                PlayerPrefs.SetInt("TipsOn", 1);
                }
            TogglePanelCanvas(canvasGroup);
            TogglePanelCanvas(inventoryInsideTip);
            TogglePanelCanvas(hotBarTip);
            
        }
    }

    public void TogglePanelCanvas(CanvasGroup canvas)
    {
        if(canvas.alpha == 0)
        {
            canvas.alpha = 1;
        }
        else
        {
            canvas.alpha = 0;
        }
    }
    public void ToggleStartItemTip()
    {
        TogglePanelCanvas(startItemTip);
    }
    public void ToggleInventory()
    {
        TogglePanelCanvas(inventoryTip);
    }
    public void ToggleRuneSelection()
    {
        TogglePanelCanvas(runeSelectionTip);
    }
    public void ToggleRunePointTip(bool yes)
    {
        if (yes) runePointTip.alpha = 1;
        else runePointTip.alpha = 0;
    }
}
