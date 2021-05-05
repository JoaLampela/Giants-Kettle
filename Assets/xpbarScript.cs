using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class xpbarScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image xpBar;
    [SerializeField] private TextMeshProUGUI levleText;
    [SerializeField] private TextMeshProUGUI xpProgressText;

    [SerializeField] private int xpGainOnKill = 5;
    [SerializeField] private int xpGainOnWaveClear = 10;
    [SerializeField] private int xpGainRoomClear = 30;

    [SerializeField] private int currentXp = 0;
    [SerializeField] private int xpReqwuirement = 100;
    [SerializeField] private int xpRequirementGrowth = 10;

    [SerializeField] private GameObject levelUpEffect;

    private GameObject player;

    [SerializeField] private GameObject levelProgressPopUp;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }
    private void Start()
    {
        Subscribe();
    }

    private void Kill(GameObject enemy)
    {
        XpPopup.Create(player.transform.position, (int)(enemy.GetComponent<EntityStats>().currentMaxHealth / 50f) + xpGainOnKill);

        Debug.Log("Kill xp");
        if(currentXp + (int)(enemy.GetComponent<EntityStats>().currentMaxHealth / 50f) >= xpReqwuirement)
        {
            int leftOver = (int)(enemy.GetComponent<EntityStats>().currentMaxHealth / 50f) - xpReqwuirement + currentXp;
            player.GetComponent<EntityStats>().level++;
            currentXp = 0;
            xpReqwuirement += xpRequirementGrowth;
            currentXp += leftOver;
        }
        else
            currentXp += (int)(enemy.GetComponent<EntityStats>().currentMaxHealth / 50f);


        if (currentXp + xpGainOnKill >= xpReqwuirement)
        {
            int leftOver = xpGainOnKill - xpReqwuirement + currentXp;
            player.GetComponent<EntityStats>().level++;
            currentXp = 0;
            xpReqwuirement += xpRequirementGrowth;
            currentXp += leftOver;
        }
        else
            currentXp += xpGainOnKill;
    }
    private void WaveClear()
    {
        XpPopup.Create(player.transform.position, xpGainOnWaveClear);

        if (currentXp + xpGainOnWaveClear >= xpReqwuirement)
        {
            int leftOver = xpGainOnKill - xpReqwuirement + currentXp;
            player.GetComponent<EntityStats>().level++;
            currentXp = 0;
            xpReqwuirement += xpRequirementGrowth;
            currentXp += leftOver;
        }
        else
            currentXp += xpGainOnWaveClear;
    }

    private void RoomClear()
    {
        XpPopup.Create(player.transform.position, xpGainRoomClear);

        if (currentXp + xpGainRoomClear >= xpReqwuirement)
        {
            int leftOver = xpGainRoomClear - xpReqwuirement + currentXp;
            player.GetComponent<EntityStats>().level++;
            currentXp = 0;
            xpReqwuirement += xpRequirementGrowth;
            currentXp += leftOver;
        }
        else
            currentXp += xpGainRoomClear;
    }

    private void Update()
    {
        if(levleText.text != player.GetComponent<EntityStats>().level.ToString())
        {
            levleText.text = player.GetComponent<EntityStats>().level.ToString();
            Instantiate(levelUpEffect, player.transform.position, player.transform.rotation);
            GameObject.Find("Game Manager").GetComponent<GameEventManager>().playerLevelUpPoints++;
        }
        xpBar.fillAmount = (float)currentXp / (float)xpReqwuirement;
        xpProgressText.text = currentXp.ToString() + " / " + xpReqwuirement.ToString();
    }

    private void Subscribe()
    {
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnRoomClear += RoomClear;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnWaveClear += WaveClear;
        player.GetComponent<EntityEvents>().OnKillEnemy += Kill;
    }

    private void Unsubscribe()
    {
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnRoomClear -= RoomClear;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnWaveClear -= WaveClear;
        player.GetComponent<EntityEvents>().OnKillEnemy -= Kill;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        levelProgressPopUp.active = false;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        levelProgressPopUp.active = true;
    }
}
