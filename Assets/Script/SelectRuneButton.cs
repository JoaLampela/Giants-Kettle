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
    private GameEventManager gameEventManager;
    [SerializeField] private NoobPanelScript noobPanelScript;


    private void Awake()
    {
        _button = runeButton.GetComponent<Button>();
        _runeIcon = runeButton.GetComponent<Image>();
        runeList = GetComponentInParent<RuneTierListObjects>();
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }

    public void OnButtonClick()
    {
        Debug.Log("Button clicked");
        runeList.IncrementScore(_rune);
        playerInventory.UseItem(new Item(_rune));
        Debug.Log("Button clicked2");
        gameEventManager.RunePicked(_rune);
        _rune = null;
        gameEventManager.ReducePlayerLevelUpPoints();
        runeList.RandomizeNewRunes();

        
    }

    public void SetNewRune(RuneObject rune)
    {
       
        _rune = rune;
        _runeIcon.sprite = _rune.iconSprite;
    }

    public void OpenPanel()
    {

    }
}
