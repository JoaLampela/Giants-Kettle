using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBarSlot : MonoBehaviour
{
    private int index;
    private GameObject inventorySlot;

    void Start()
    {
        index = transform.GetSiblingIndex();
        inventorySlot = GameObject.FindGameObjectWithTag("InventoryPanel").transform.GetChild(index).gameObject;
    }

    void Update()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = inventorySlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
    }
}
