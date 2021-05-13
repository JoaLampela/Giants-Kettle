using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItemSelectScript : MonoBehaviour
{
    public ItemObject sword;
    public ItemObject shield;
    public ItemObject twoHandedSword;
    public ItemObject staff;


    public ItemObject swordL;
    public ItemObject shieldL;
    public ItemObject twoHandedSwordL;
    public ItemObject staffL;

    [SerializeField] private NoobPanelScript noobPanelScript;
    private GameEventManager gameEventManager;

    private Inventory inventory;

    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void SelectSwordSword()
    {

        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            Item item = new Item(swordL);
            inventory.rightHand.icon.sprite = item.item.iconSprite;
            inventory.rightHand._item = item;
            inventory.Equip(item, inventory.rightHand);
        }
        else
        {
            Item item = new Item(sword);
            inventory.rightHand.icon.sprite = item.item.iconSprite;
            inventory.rightHand._item = item;
            inventory.Equip(item, inventory.rightHand);
        }

        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            Item item = new Item(swordL);
            inventory.leftHand.icon.sprite = item.item.iconSprite;
            inventory.leftHand._item = item;
            inventory.Equip(item, inventory.leftHand);
        }
        else 
        {
            Item item = new Item(sword);
            inventory.leftHand.icon.sprite = item.item.iconSprite;
            inventory.leftHand._item = item;
            inventory.Equip(item, inventory.leftHand);
        }
        gameObject.SetActive(false);

        noobPanelScript.ToggleStartItemTip();
        gameEventManager.ToggleRuneSelectionView();
    }

    public void SelectShieldShield()
    {

        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            Item item = new Item(shieldL);
            inventory.rightHand.icon.sprite = item.item.iconSprite;
            inventory.rightHand._item = item;
            inventory.Equip(item, inventory.rightHand);
        }
        else
        {
            Item item = new Item(shield);
            inventory.rightHand.icon.sprite = item.item.iconSprite;
            inventory.rightHand._item = item;
            inventory.Equip(item, inventory.rightHand);
        }

        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            Item item = new Item(shieldL);
            inventory.leftHand.icon.sprite = item.item.iconSprite;
            inventory.leftHand._item = item;
            inventory.Equip(item, inventory.leftHand);
        }
        else
        {
            Item item = new Item(shield);
            inventory.leftHand.icon.sprite = item.item.iconSprite;
            inventory.leftHand._item = item;
            inventory.Equip(item, inventory.leftHand);
        }
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
        gameEventManager.ToggleRuneSelectionView();
    }

    public void SelectShieldSword()
    {

        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            Item item = new Item(swordL);
            inventory.rightHand.icon.sprite = item.item.iconSprite;
            inventory.rightHand._item = item;
            inventory.Equip(item, inventory.rightHand);
        }
        else
        {
            Item item = new Item(sword);
            inventory.rightHand.icon.sprite = item.item.iconSprite;
            inventory.rightHand._item = item;
            inventory.Equip(item, inventory.rightHand);
        }



        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            Item item = new Item(shieldL);
            inventory.leftHand.icon.sprite = item.item.iconSprite;
            inventory.leftHand._item = item;
            inventory.Equip(item, inventory.leftHand);
        }

        else 
        {
            Item item = new Item(shield);
            inventory.leftHand.icon.sprite = item.item.iconSprite;
            inventory.leftHand._item = item;
            inventory.Equip(item, inventory.leftHand);
        }
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
        gameEventManager.ToggleRuneSelectionView();
    }

    public void SelectStaff()
    {
        
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) inventory.UseItem(new Item(staffL));
        else inventory.UseItem(new Item(staff));
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
        gameEventManager.ToggleRuneSelectionView();

    }
    public void Select2HSword()
    {
        
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) inventory.UseItem(new Item(twoHandedSwordL));
        else inventory.UseItem(new Item(twoHandedSword)); 
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
        gameEventManager.ToggleRuneSelectionView();
    }
}
