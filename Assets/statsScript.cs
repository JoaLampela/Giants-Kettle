using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class statsScript : MonoBehaviour
{
    [SerializeField] private TotalGameStats totalGameStats;

    [SerializeField] private TextMeshProUGUI totalPlayTimeText;
    [SerializeField] private TextMeshProUGUI totalenemiesKilledText;
    [SerializeField] private TextMeshProUGUI totalfloorsClearedText;
    [SerializeField] private TextMeshProUGUI totalRoomsClearedText;
    [SerializeField] private TextMeshProUGUI totalRunesPickedUpText;
    [SerializeField] private TextMeshProUGUI totalItemsPickedUpText;
    [SerializeField] private TextMeshProUGUI totalDamageDealtText;
    [SerializeField] private TextMeshProUGUI HighestDamageDealtText;
    [SerializeField] private TextMeshProUGUI AttackwithMostDamageText;
    [SerializeField] private TextMeshProUGUI totalDeathCountText;
    [SerializeField] private TextMeshProUGUI totalhurtEnemiesText;
    [SerializeField] private TextMeshProUGUI totalBossesKilledText;
    [SerializeField] private TextMeshProUGUI causeOfNightmaresText;
    [SerializeField] private TextMeshProUGUI highestKillCountText;
    [SerializeField] private TextMeshProUGUI deepestFloorText;



    private void Start()
    {
        totalPlayTimeText.text = CalculateGameTime(totalGameStats.totalGameTime);
        totalenemiesKilledText.text = totalGameStats.killedEnemiesTotal.ToString();
        totalfloorsClearedText.text = totalGameStats.clearedFloors.ToString();
        totalRoomsClearedText.text = totalGameStats.clearedRooms.ToString();
        totalRunesPickedUpText.text = totalGameStats.runesPicked.ToString();
        totalItemsPickedUpText.text = totalGameStats.totalItemsFound.ToString();
        totalDamageDealtText.text = totalGameStats.totalDamageDealt.ToString();
        HighestDamageDealtText.text = totalGameStats.highestDamageInGame.ToString();
        AttackwithMostDamageText.text = totalGameStats.highestDamageAttack.ToString();
        totalDeathCountText.text = totalGameStats.totalDeathCount.ToString();
        totalhurtEnemiesText.text = totalGameStats.totalHits.ToString();
        totalBossesKilledText.text = totalGameStats.totalBossesKilled.ToString();
        highestKillCountText.text = totalGameStats.bestKilledEnemies.ToString();
        deepestFloorText.text = totalGameStats.deepestFloor.ToString();

        int temp = 0;
        string killer = " ";
        if (totalGameStats.killedByGoblin > temp) {
            killer = "Goblin";
            temp = totalGameStats.killedByGoblin;
        }
        if (totalGameStats.killedByFlyer > temp)
        {
            killer = "Flyer";
            temp = totalGameStats.killedByFlyer;
        }
        if (totalGameStats.killedBySkeleton > temp)
        {
            killer = "Skeleton";
            temp = totalGameStats.killedBySkeleton;
        }
        if (totalGameStats.killedBySummoner > temp)
        {
            killer = "Summoner";
            temp = totalGameStats.killedBySummoner;
        }
        if (totalGameStats.killedHoglins > temp)
        {
            killer = "Hoglin";
        }

        causeOfNightmaresText.text = killer;
    }

    private string CalculateGameTime(int seconds)
    {
        int minutes =(int) (seconds / 60f);
        seconds = seconds - minutes * 60;
        int hours = (int)(minutes / 60f);
        minutes = minutes - hours * 60;

        return hours + "h " + minutes + "min " + seconds + "s";
    }
}
