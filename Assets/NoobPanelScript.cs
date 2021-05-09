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
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
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
