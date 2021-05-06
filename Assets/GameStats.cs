using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    private Dictionary<RuneObject, int> collectedRunes;
    private Dictionary<EquipmentObject, int> collectedEquipments;

    public int killedEnemiesTotal = 0;
    public int killedGoblins = 0;
    public int killedFlyers = 0;
    public int killedSkeletons = 0;
    public int killedSummoners = 0;
    public int killedHoglins = 0;

    public int clearedFloors = 0;
    public int clearedRooms = 0;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        collectedRunes = new Dictionary<RuneObject, int>();
        collectedEquipments = new Dictionary<EquipmentObject, int>();
        Subscribe();
    }
    public void AddRune(RuneObject rune)
    {
        if (collectedRunes.ContainsKey(rune)) collectedRunes[rune]++;
        else collectedRunes.Add(rune, 1);

        foreach(RuneObject temp in collectedRunes.Keys)
        {
            Debug.Log("Rune: " + temp + " count: " + collectedRunes[temp]);
        }
    }

    public void AddEquipment(EquipmentObject equipment)
    {
        if (collectedEquipments.ContainsKey(equipment)) collectedEquipments[equipment]++;
        else collectedEquipments.Add(equipment, 1);

        foreach (EquipmentObject temp in collectedEquipments.Keys)
        {
            Debug.Log("Equipment: " + temp + " count: " + collectedEquipments[temp]);
        }
    }

    public void AddKilledEnemy(GameObject entity)
    {
        if (entity.GetComponent<EntityStats>().entityName == "Goblin") killedGoblins++;
        else if (entity.GetComponent<EntityStats>().entityName == "Flyer") killedFlyers++;
        else if (entity.GetComponent<EntityStats>().entityName == "Skelebro") killedSkeletons++;
        else if (entity.GetComponent<EntityStats>().entityName == "Summoner") killedSummoners++;
        else if (entity.GetComponent<EntityStats>().entityName == "Hoglon") killedHoglins++;
    }

    public void AddClearedRoom()
    {
        clearedRooms++;
    }
    public void AddClearedFloor()
    {
        clearedFloors++;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        player.GetComponent<EntityEvents>().OnKillEnemy += AddKilledEnemy;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnRunePicked += AddRune;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnEquipmentDropepd += AddEquipment;
    }
    private void Unsubscribe()
    {
        player.GetComponent<EntityEvents>().OnKillEnemy -= AddKilledEnemy;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnRunePicked -= AddRune;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnEquipmentDropepd -= AddEquipment;
    }

    public void EndGameSaveStats()
    {
        Debug.Log("Saving game stats");
    }
}
