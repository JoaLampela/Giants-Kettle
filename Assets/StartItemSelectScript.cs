using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItemSelectScript : MonoBehaviour
{
    public ItemOnGround itemOnGround;
    public ItemObject sword;
    public ItemObject shield;
    public ItemObject twoHandedSword;
    public ItemObject staff;


    public ItemObject swordL;
    public ItemObject shieldL;
    public ItemObject twoHandedSwordL;
    public ItemObject staffL;

    [SerializeField] private NoobPanelScript noobPanelScript;

    public void SelectSwordSword()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position , Quaternion.identity);
        groundItem.SetItem(new Item(sword));
        if(GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(swordL));
        groundItem.pickedUp = true;

        groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(sword));
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(swordL));
        groundItem.pickedUp = true;

        Time.timeScale = 1f;
        gameObject.SetActive(false);

        noobPanelScript.ToggleStartItemTip();
    }

    public void SelectShieldShield()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(shield));
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(swordL));
        groundItem.pickedUp = true;

        groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(shield));
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(shieldL));
        groundItem.pickedUp = true;

        Time.timeScale = 1f;
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
    }

    public void SelectShieldSword()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(shield));
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(shieldL));
        groundItem.pickedUp = true;

        groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(sword));
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(swordL));
        groundItem.pickedUp = true;

        Time.timeScale = 1f;
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
    }

    public void SelectStaff()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(staff));
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(staffL));
        groundItem.pickedUp = true;

        Time.timeScale = 1f;
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
    }
    public void Select2HSword()
    {
        ItemOnGround groundItem = Instantiate(itemOnGround, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        groundItem.SetItem(new Item(twoHandedSword));
        if (GameObject.Find("Game Manager").GetComponent<GameEventManager>().difficulty == GameDifficultyManagerScript.Difficulty.Lunatic) groundItem.SetItem(new Item(twoHandedSwordL));
        groundItem.pickedUp = true;

        Time.timeScale = 1f;
        gameObject.SetActive(false);
        noobPanelScript.ToggleStartItemTip();
    }
}
