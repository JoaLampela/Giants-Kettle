using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RunePanelScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] RuneObject rune;
    [SerializeField] private Image runeImage;
    GameObject toolTip;
    int score;


    private void Awake()
    {
        toolTip = GameObject.Find("Tooltip");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENter");
        if (score >= 3) 
            toolTip.GetComponent<menuToolTip>().DisplayText(rune.description);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        toolTip.GetComponent<CanvasGroup>().alpha = 0;
    }

    void Start()
    {
        runeImage.sprite = rune.iconSprite;
        score = GameObject.Find("TotalGameStats").GetComponent<TotalGameStats>().GetRuneScore(rune);
        count.text = score.ToString();

        if (score < 3) runeImage.color = Color.black;
    }
}
