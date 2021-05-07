using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RunePanelScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] RuneObject rune;
    [SerializeField] private Image runeImage;

    void Start()
    {
        runeImage.sprite = rune.iconSprite;
        int score = GameObject.Find("TotalGameStats").GetComponent<TotalGameStats>().GetRuneScore(rune);
        count.text = score.ToString();

        if (score == 0) runeImage.color = Color.black;
    }
}
