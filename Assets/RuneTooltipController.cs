using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RuneTooltipController : MonoBehaviour
{
    [SerializeField] private GameObject runeToolTip1;
    [SerializeField] private GameObject runeToolTip2;
    [SerializeField] private GameObject runeToolTip3;
    [SerializeField] private GameObject runeToolTip4;
    [SerializeField] private GameObject runeToolTip5;
    [SerializeField] private GameObject runeToolTip6;
    private UiButtonClick parentInventorySlot;
    [SerializeField] private Sprite defaultRuneSprite;


    private void Awake()
    {
        parentInventorySlot = GetComponentInParent<UiButtonClick>();
    }
    public void DisplayToolTip()
    {
        if (parentInventorySlot._item._runeList.Length == 1)
        {
            Item item = parentInventorySlot._item;
            runeToolTip1.SetActive(true);
            if (item._runeList[0] != null) runeToolTip1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item._runeList[0].iconSprite;
            else runeToolTip1.transform.GetChild(0).GetComponent<Image>().sprite = defaultRuneSprite;
        }
        else if (parentInventorySlot._item._runeList.Length == 2)
        {

            runeToolTip2.SetActive(true);
            Item item = parentInventorySlot._item;
            if (item._runeList[0] != null) runeToolTip2.transform.GetChild(0).GetComponent<Image>().sprite = item._runeList[0].iconSprite;
            else runeToolTip2.transform.GetChild(0).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[1] != null) runeToolTip2.transform.GetChild(1).GetComponent<Image>().sprite = item._runeList[1].iconSprite;
            else runeToolTip2.transform.GetChild(1).GetComponent<Image>().sprite = defaultRuneSprite;
        }
        else if (parentInventorySlot._item._runeList.Length == 3)
        {
            runeToolTip3.SetActive(true);
            Item item = parentInventorySlot._item;
            if (item._runeList[0] != null) runeToolTip3.transform.GetChild(0).GetComponent<Image>().sprite = item._runeList[0].iconSprite;
            else runeToolTip3.transform.GetChild(0).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[1] != null) runeToolTip3.transform.GetChild(1).GetComponent<Image>().sprite = item._runeList[1].iconSprite;
            else runeToolTip3.transform.GetChild(1).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[2] != null) runeToolTip3.transform.GetChild(2).GetComponent<Image>().sprite = item._runeList[2].iconSprite;
            else runeToolTip3.transform.GetChild(2).GetComponent<Image>().sprite = defaultRuneSprite;
        }
        else if (parentInventorySlot._item._runeList.Length == 4)
        {
            runeToolTip4.SetActive(true);
            Item item = parentInventorySlot._item;
            if (item._runeList[0] != null) runeToolTip4.transform.GetChild(0).GetComponent<Image>().sprite = item._runeList[0].iconSprite;
            else runeToolTip4.transform.GetChild(0).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[1] != null) runeToolTip4.transform.GetChild(1).GetComponent<Image>().sprite = item._runeList[1].iconSprite;
            else runeToolTip4.transform.GetChild(1).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[2] != null) runeToolTip4.transform.GetChild(2).GetComponent<Image>().sprite = item._runeList[2].iconSprite;
            else runeToolTip4.transform.GetChild(2).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[3] != null) runeToolTip4.transform.GetChild(3).GetComponent<Image>().sprite = item._runeList[3].iconSprite;
            else runeToolTip4.transform.GetChild(3).GetComponent<Image>().sprite = defaultRuneSprite;
        }
        else if (parentInventorySlot._item._runeList.Length == 5)
        {
            runeToolTip5.SetActive(true);
            Item item = parentInventorySlot._item;
            if (item._runeList[0] != null) runeToolTip5.transform.GetChild(0).GetComponent<Image>().sprite = item._runeList[0].iconSprite;
            else runeToolTip5.transform.GetChild(0).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[1] != null) runeToolTip5.transform.GetChild(1).GetComponent<Image>().sprite = item._runeList[1].iconSprite;
            else runeToolTip5.transform.GetChild(1).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[2] != null) runeToolTip5.transform.GetChild(2).GetComponent<Image>().sprite = item._runeList[2].iconSprite;
            else runeToolTip5.transform.GetChild(2).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[3] != null) runeToolTip5.transform.GetChild(3).GetComponent<Image>().sprite = item._runeList[3].iconSprite;
            else runeToolTip5.transform.GetChild(3).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[4] != null) runeToolTip5.transform.GetChild(4).GetComponent<Image>().sprite = item._runeList[4].iconSprite;
            else runeToolTip5.transform.GetChild(4).GetComponent<Image>().sprite = defaultRuneSprite;
        }
        else if (parentInventorySlot._item._runeList.Length == 6)
        {
            runeToolTip6.SetActive(true);
            Item item = parentInventorySlot._item;
            if (item._runeList[0] != null) runeToolTip6.transform.GetChild(0).GetComponent<Image>().sprite = item._runeList[0].iconSprite;
            else runeToolTip6.transform.GetChild(0).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[1] != null) runeToolTip6.transform.GetChild(1).GetComponent<Image>().sprite = item._runeList[1].iconSprite;
            else runeToolTip6.transform.GetChild(1).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[2] != null) runeToolTip6.transform.GetChild(2).GetComponent<Image>().sprite = item._runeList[2].iconSprite;
            else runeToolTip6.transform.GetChild(2).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[3] != null) runeToolTip6.transform.GetChild(3).GetComponent<Image>().sprite = item._runeList[3].iconSprite;
            else runeToolTip6.transform.GetChild(3).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[4] != null) runeToolTip6.transform.GetChild(4).GetComponent<Image>().sprite = item._runeList[4].iconSprite;
            else runeToolTip6.transform.GetChild(4).GetComponent<Image>().sprite = defaultRuneSprite;
            if (item._runeList[5] != null) runeToolTip6.transform.GetChild(5).GetComponent<Image>().sprite = item._runeList[5].iconSprite;
            else runeToolTip6.transform.GetChild(5).GetComponent<Image>().sprite = defaultRuneSprite;
        }
    }
    public void HideToolTip()
    {
        runeToolTip1.SetActive(false);
        runeToolTip2.SetActive(false);
        runeToolTip3.SetActive(false);
        runeToolTip4.SetActive(false);
        runeToolTip5.SetActive(false);
        runeToolTip6.SetActive(false);
    }
}
