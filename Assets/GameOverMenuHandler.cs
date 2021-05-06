using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject bonePileRight;
    [SerializeField] private GameObject bonePileLeft;
    [SerializeField] private GameStats gameStats;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI floorText;


    [SerializeField] private int BonepilePosYStart = 0;
    [SerializeField] private int BonepilePosYEnd = 0;
    

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator BeginningOfTheEndKills()
    {
        float waitTimeStart = 2f;
        int startKills = 0;
        
        Debug.Log("StartedCR " + gameStats.killedEnemiesTotal + " " + startKills);
        while (startKills < gameStats.killedEnemiesTotal)
        {
            Debug.Log(startKills + " wait time " + waitTimeStart);
            startKills++;
            Debug.Log(startKills);
            waitTimeStart /= 2f;
            killsText.text = " Kills:  " + startKills.ToString();
            if(bonePileLeft.transform.position.y < BonepilePosYEnd)
            {
                bonePileLeft.transform.position = new Vector2(bonePileLeft.transform.position.x, bonePileLeft.transform.position.y + 2);
                bonePileRight.transform.position = new Vector2(bonePileRight.transform.position.x, bonePileRight.transform.position.y + 2);
            }
            yield return new WaitForSeconds(0);
        }
    }

    public IEnumerator BeginningOFTheEndFloors()
    {
        int startFloors = 0;
        while(startFloors < gameStats.clearedFloors)
        {
            startFloors++;
            floorText.text = "Reached\nfloor\n" + startFloors;
            yield return new WaitForSeconds(0);
        }
    }
}
