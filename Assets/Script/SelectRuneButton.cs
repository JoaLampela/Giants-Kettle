using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRuneButton : MonoBehaviour
{
    [SerializeField] private GameObject runeButton;
    private Button _button;
    private Image _runeIcon;
    private RuneObject _rune;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private RuneTierListObjects runeList;

    private void Awake()
    {
        _button = runeButton.GetComponent<Button>();
        _runeIcon = runeButton.GetComponent<Image>();
        runeList = GetComponentInParent<RuneTierListObjects>();
    }

    public void OnButtonClick()
    {
        runeList.IncrementScore(_rune);
        Debug.Log(runeButton);
        playerInventory.NewItem(new Item(_rune));
        _rune = null;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().ReducePlayerLevelUpPoints();
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().playerLevelUpPoints > 0)
        {
            runeList.RandomizeNewRunes();
        }
    }

    public void SetNewRune(RuneObject rune)
    {
       
        _rune = rune;
        Debug.Log("set new rune " + _rune);
        Debug.Log("set new rune " + _rune.iconSprite);
        Debug.Log("set new rune " + _rune + " " + _rune.iconSprite);

        _runeIcon.sprite = _rune.iconSprite;
    }

    public void OpenPanel()
    {

    }
}
