using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectorPanel : MonoBehaviour
{
    [SerializeField] private Image previewImage;
    private CanvasGroup canvasGroup;
    [SerializeField] private Image rune1;
    [SerializeField] private Image rune2;
    [SerializeField] private Image rune3;
    [SerializeField] private Image rune4;
    [SerializeField] private Image rune5;
    [SerializeField] private Image rune6;
    [SerializeField] private Sprite defaultImage;

    [SerializeField] private GameObject runeSlot1;
    [SerializeField] private GameObject runeSlot2;
    [SerializeField] private GameObject runeSlot3;
    [SerializeField] private GameObject runeSlot4;
    [SerializeField] private GameObject runeSlot5;
    [SerializeField] private GameObject runeSlot6;

    [SerializeField] private Image runeEffect1;
    [SerializeField] private Image runeEffect2;
    [SerializeField] private Image runeEffect3;
    [SerializeField] private Image runeEffect4;

    public void SetPreviewImage(Sprite newSprite)
    {
        previewImage.sprite = newSprite;
    }

    public void DisapleInspector()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    public void EnableInspector()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    [System.Obsolete]
    public void InspectorSetRuneSlots(Item item)
    {
        if(item._runeList.Length >= 0)
        {
            if(item._runeList.Length == 0)
            {
                runeSlot1.active = false;
                runeSlot2.active = false;
                runeSlot3.active = false;
                runeSlot4.active = false;
                runeSlot5.active = false;
                runeSlot6.active = false;
            }
            if(item._runeList.Length == 1)
            {
                runeSlot1.active = true;
                runeSlot2.active = false;
                runeSlot3.active = false;
                runeSlot4.active = false;
                runeSlot5.active = false;
                runeSlot6.active = false;
                if (item._runeList[0] != null) rune1.sprite = item._runeList[0].iconSprite;
                else rune1.sprite = defaultImage;
            }
            if (item._runeList.Length == 2)
            {
                runeSlot1.active = true;
                runeSlot2.active = true;
                runeSlot3.active = false;
                runeSlot4.active = false;
                runeSlot5.active = false;
                runeSlot6.active = false;
                if (item._runeList[0] != null) rune1.sprite = item._runeList[0].iconSprite;
                else rune1.sprite = defaultImage;
                if (item._runeList[1] != null) rune2.sprite = item._runeList[1].iconSprite;
                else rune2.sprite = defaultImage;
            }
            if (item._runeList.Length == 3)
            {
                runeSlot1.active = true;
                runeSlot2.active = true;
                runeSlot3.active = true;
                runeSlot4.active = false;
                runeSlot5.active = false;
                runeSlot6.active = false;
                if (item._runeList[0] != null) rune1.sprite = item._runeList[0].iconSprite;
                else rune1.sprite = defaultImage;
                if (item._runeList[1] != null) rune2.sprite = item._runeList[1].iconSprite;
                else rune2.sprite = defaultImage;
                if (item._runeList[2] != null) rune3.sprite = item._runeList[2].iconSprite;
                else rune3.sprite = defaultImage;
            }
            if (item._runeList.Length == 4)
            {
                runeSlot1.active = true;
                runeSlot2.active = true;
                runeSlot3.active = true;
                runeSlot4.active = true;
                runeSlot5.active = false;
                runeSlot6.active = false;
                if (item._runeList[0] != null) rune1.sprite = item._runeList[0].iconSprite;
                else rune1.sprite = defaultImage;
                if (item._runeList[1] != null) rune2.sprite = item._runeList[1].iconSprite;
                else rune2.sprite = defaultImage;
                if (item._runeList[2] != null) rune3.sprite = item._runeList[2].iconSprite;
                else rune3.sprite = defaultImage;
                if (item._runeList[3] != null) rune4.sprite = item._runeList[3].iconSprite;
                else rune4.sprite = defaultImage;
            }
            if (item._runeList.Length == 5)
            {
                runeSlot1.active = true;
                runeSlot2.active = true;
                runeSlot3.active = true;
                runeSlot4.active = true;
                runeSlot5.active = true;
                runeSlot6.active = false;
                if (item._runeList[0] != null) rune1.sprite = item._runeList[0].iconSprite;
                else rune1.sprite = defaultImage;
                if (item._runeList[1] != null) rune2.sprite = item._runeList[1].iconSprite;
                else rune2.sprite = defaultImage;
                if (item._runeList[2] != null) rune3.sprite = item._runeList[2].iconSprite;
                else rune3.sprite = defaultImage;
                if (item._runeList[3] != null) rune4.sprite = item._runeList[3].iconSprite;
                else rune4.sprite = defaultImage;
                if (item._runeList[4] != null) rune5.sprite = item._runeList[4].iconSprite;
                else rune5.sprite = defaultImage;
            }
            if (item._runeList.Length == 6)
            {
                runeSlot1.active = true;
                runeSlot2.active = true;
                runeSlot3.active = true;
                runeSlot4.active = true;
                runeSlot5.active = true;
                runeSlot6.active = true;
                if (item._runeList[0] != null) rune1.sprite = item._runeList[0].iconSprite;
                else rune1.sprite = defaultImage;
                if (item._runeList[1] != null) rune2.sprite = item._runeList[1].iconSprite;
                else rune2.sprite = defaultImage;
                if (item._runeList[2] != null) rune3.sprite = item._runeList[2].iconSprite;
                else rune3.sprite = defaultImage;
                if (item._runeList[3] != null) rune4.sprite = item._runeList[3].iconSprite;
                else rune4.sprite = defaultImage;
                if (item._runeList[4] != null) rune5.sprite = item._runeList[4].iconSprite;
                else rune5.sprite = defaultImage;
                if (item._runeList[5] != null) rune6.sprite = item._runeList[5].iconSprite;
                else rune6.sprite = defaultImage;
            }

        }
    }

    public void InspectorSetRuneEffects(Item item)
    {
        if(item.runeEffects.Length != 0)
        {
            if(item.runeEffects.Length == 1)
            {
                runeEffect1.sprite = item.runeEffects[0];
                runeEffect2.sprite = defaultImage;
                runeEffect3.sprite = defaultImage;
                runeEffect4.sprite = defaultImage;

            }
            if (item.runeEffects.Length == 2)
            {
                runeEffect1.sprite = item.runeEffects[0];
                runeEffect2.sprite = item.runeEffects[1];
                runeEffect3.sprite = defaultImage;
                runeEffect4.sprite = defaultImage;

            }
            if (item.runeEffects.Length == 3)
            {
                Debug.Log(item.runeEffects[0]);
                runeEffect1.sprite = item.runeEffects[0];
                runeEffect2.sprite = item.runeEffects[1];
                runeEffect3.sprite = item.runeEffects[2];
                runeEffect4.sprite = defaultImage;

            }
            if (item.runeEffects.Length == 4)
            {
                runeEffect1.sprite = item.runeEffects[0];
                runeEffect2.sprite = item.runeEffects[1];
                runeEffect3.sprite = item.runeEffects[2];
                runeEffect4.sprite = item.runeEffects[3];

            }
            
        }
        else
        {
            runeEffect1.sprite = defaultImage;
            runeEffect2.sprite = defaultImage;
            runeEffect3.sprite = defaultImage;
            runeEffect4.sprite = defaultImage;
        }
    }
}
