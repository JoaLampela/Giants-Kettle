using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTierListObjects : MonoBehaviour
{
    [SerializeField] private GameObject rune1;
    [SerializeField] private GameObject rune2;
    [SerializeField] private GameObject rune3;

    public RuneObject defaultRune;

    public RuneObject[] spiritRunesTier1;
    public RuneObject[] spiritRunesTier2;
    public RuneObject[] spiritRunesTier3;

    public RuneObject[] vitalityRunesTier1;
    public RuneObject[] vitalityRunesTier2;
    public RuneObject[] vitalityRunesTier3;

    public RuneObject[] agilityRunesTier1;
    public RuneObject[] agilityRunesTier2;
    public RuneObject[] agilityRunesTier3;

    public RuneObject[] powerRunesTier1;
    public RuneObject[] powerRunesTier2;
    public RuneObject[] powerRunesTier3;

    public RuneObject[] spiritVitalityRunesTier1;
    public RuneObject[] spiritVitalityRunesTier2;
    public RuneObject[] spiritVitalityRunesTier3;

    public RuneObject[] spiritAgilityRunesTier1;
    public RuneObject[] spiritAgilityRunesTier2;
    public RuneObject[] spiritAgilityRunesTier3;

    public RuneObject[] spiritPowerRunesTier1;
    public RuneObject[] spiritPowerRunesTier2;
    public RuneObject[] spiritPowerRunesTier3;

    public RuneObject[] vitalityAgilityRunesTier1;
    public RuneObject[] vitalityAgilityRunesTier2;
    public RuneObject[] vitalityAgilityRunesTier3;

    public RuneObject[] vitalityPowerRunesTier1;
    public RuneObject[] vitalityPowerRunesTier2;
    public RuneObject[] vitalityPowerRunesTier3;

    public RuneObject[] agilityPowerRunesTier1;
    public RuneObject[] agilityPowerRunesTier2;
    public RuneObject[] agilityPowerRunesTier3;

    public int spiritScore = 1;
    public int vitalityScore = 1;
    public int agilityScore = 1;
    public int powerScore = 1;

    public int spiritVitalityScore = 0;
    public int spiritAgilityScore = 0;
    public int spiritPowerScore = 0;
    public int vitalityAgilityScore = 0;
    public int vitalityPowerScore = 0;
    public int agilityPowerScore = 0;

    public int tier2Requirement = 5;
    public int tier3Requirement = 10;

    public enum Runes
    {
        spirit,
        vitality,
        agility,
        power,
        spiritVitality,
        spiritAgility,
        spiritPower,
        vitalityAgility,
        vitalityPower,
        agilityPower,
        empty
    }
    public enum RuneRarity
    {
        basic,
        refined,
        perfected
    }

    public void IncrementScore(Runes rune)
    {
        switch(rune)
        {
            case Runes.spirit:
                if (vitalityScore > spiritScore) spiritVitalityScore++;
                if (agilityScore > spiritScore) spiritAgilityScore++;
                if (powerScore > spiritScore) spiritPowerScore++;
                spiritScore++;
                break;
            case Runes.vitality:
                if (spiritScore > vitalityScore) spiritVitalityScore++;
                if (agilityScore > vitalityScore) vitalityAgilityScore++;
                if (powerScore > vitalityScore) vitalityPowerScore++;
                vitalityScore++;
                break;
            case Runes.agility:
                if (spiritScore > agilityScore) spiritAgilityScore++;
                if (vitalityScore > agilityScore) vitalityAgilityScore++;
                if (powerScore > agilityScore) agilityPowerScore++;
                agilityScore++;
                break;
            case Runes.power:
                if (spiritScore > powerScore) spiritPowerScore++;
                if (vitalityScore > powerScore) vitalityPowerScore++;
                if (agilityScore > powerScore) agilityPowerScore++;
                powerScore++;
                break;
        }
    }

    public void IncrementScore(RuneObject rune)
    {
        switch (rune.runeType)
        {
            case RuneObject.RuneType.spirit:
                if (vitalityScore > spiritScore) spiritVitalityScore++;
                if (agilityScore > spiritScore) spiritAgilityScore++;
                if (powerScore > spiritScore) spiritPowerScore++;
                spiritScore++;
                break;
            case RuneObject.RuneType.vitality:
                if (spiritScore > vitalityScore) spiritVitalityScore++;
                if (agilityScore > vitalityScore) vitalityAgilityScore++;
                if (powerScore > vitalityScore) vitalityPowerScore++;
                vitalityScore++;
                break;
            case RuneObject.RuneType.agility:
                if (spiritScore > agilityScore) spiritAgilityScore++;
                if (vitalityScore > agilityScore) vitalityAgilityScore++;
                if (powerScore > agilityScore) agilityPowerScore++;
                agilityScore++;
                break;
            case RuneObject.RuneType.power:
                if (spiritScore > powerScore) spiritPowerScore++;
                if (vitalityScore > powerScore) vitalityPowerScore++;
                if (agilityScore > powerScore) agilityPowerScore++;
                powerScore++;
                break;
            case RuneObject.RuneType.spiritVitality:
                IncrementScore(Runes.spirit);
                IncrementScore(Runes.vitality);
                break;
            case RuneObject.RuneType.spiritAgility:
                IncrementScore(Runes.spirit);
                IncrementScore(Runes.agility);
                break;
            case RuneObject.RuneType.spiritPower:
                IncrementScore(Runes.spirit);
                IncrementScore(Runes.power);
                break;
            case RuneObject.RuneType.vitalityAgility:
                IncrementScore(Runes.agility);
                IncrementScore(Runes.vitality);
                break;
            case RuneObject.RuneType.vitalityPower:
                IncrementScore(Runes.power);
                IncrementScore(Runes.vitality);
                break;
            case RuneObject.RuneType.agilitypower:
                IncrementScore(Runes.agility);
                IncrementScore(Runes.power);
                break;
        }
    }

    private int GetScore(Runes rune)
    {
        switch (rune)
        {
            case Runes.spirit:
                return spiritScore;
            case Runes.vitality:
                return vitalityScore;
            case Runes.agility:
                return agilityScore;
            case Runes.power:
                return powerScore;
            case Runes.spiritVitality:
                if (spiritVitalityScore > 5) return spiritVitalityScore;
                else return 0;
            case Runes.spiritAgility:
                if (spiritAgilityScore > 5) return spiritAgilityScore;
                else return 0;
            case Runes.spiritPower:
                if (spiritPowerScore > 5) return spiritPowerScore;
                else return 0;
            case Runes.vitalityAgility:
                if (vitalityAgilityScore > 5) return vitalityAgilityScore;
                else return 0;
            case Runes.vitalityPower:
                if (vitalityPowerScore > 5) return vitalityPowerScore;
                else return 0;
            case Runes.agilityPower:
                if (agilityPowerScore > 5) return agilityPowerScore;
                else return 0;
        }
        return 0;
    }
    private void Update()
    {
        RuneObject rune = defaultRune;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeNewRunes();
            rune = GetRandomRune();
            Debug.Log(rune);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            IncrementScore(Runes.spirit);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            IncrementScore(Runes.vitality);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            IncrementScore(Runes.agility);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            IncrementScore(Runes.power);
        }


    }

    public RuneObject GetRandomRune()
    {
        int spiritVitalityScore1 = spiritVitalityScore;
        int spiritAgilityScore1 = spiritAgilityScore;
        int spiritPowerScore1 = spiritPowerScore;
        int vitalityAgilityScore1 = vitalityAgilityScore;
        int vitalityPowerScore1 = vitalityPowerScore;
        int agilityPowerScore1 = agilityPowerScore;

        if (spiritVitalityScore1 < 6) spiritVitalityScore1 = 0;
        if (spiritAgilityScore1 < 6) spiritAgilityScore1 = 0;
        if (spiritPowerScore1 < 6) spiritPowerScore1 = 0;
        if (vitalityAgilityScore1 < 6) vitalityAgilityScore1 = 0;
        if (vitalityPowerScore1 < 6) vitalityPowerScore1 = 0;
        if (agilityPowerScore1 < 6) agilityPowerScore1 = 0;

        int upperLimit = (1 + spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore1 + spiritAgilityScore1 + spiritPowerScore1 + vitalityAgilityScore1 + vitalityPowerScore1 + agilityPowerScore1);
        int selectedRuneNumber = Random.Range(1, upperLimit);
        Runes selectedRuneType = Runes.empty;
        if (selectedRuneNumber <= spiritScore) selectedRuneType = Runes.spirit;
        else if (selectedRuneNumber <= spiritScore + vitalityScore) selectedRuneType = Runes.vitality;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore) selectedRuneType = Runes.agility;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore) selectedRuneType = Runes.power;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore1) selectedRuneType = Runes.spiritVitality;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore1 + spiritAgilityScore1) selectedRuneType = Runes.spiritAgility;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore1 + spiritAgilityScore1 + spiritPowerScore1) selectedRuneType = Runes.spiritPower;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore1 + spiritAgilityScore1 + spiritPowerScore1 + vitalityAgilityScore1) selectedRuneType = Runes.vitalityAgility;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore1 + spiritAgilityScore1 + spiritPowerScore1 + vitalityAgilityScore1 + vitalityPowerScore1) selectedRuneType = Runes.vitalityPower;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore1 + spiritAgilityScore1 + spiritPowerScore1 + vitalityAgilityScore1 + vitalityPowerScore1 + agilityPowerScore1) selectedRuneType = Runes.agilityPower;


        int runeScore = GetScore(selectedRuneType);
        int tier1Propability = 0;
        int tier2Propability = 0;
        int tier3Propability = 0;

        if(runeScore < 6)
        {
            tier1Propability = 100;
            tier2Propability = 0;
            tier3Propability = 0;
        }
        else if(runeScore < 11)
        {
            tier1Propability = 75 - runeScore * 5;
            tier2Propability = 25 + runeScore * 5;
            tier3Propability = 0;
        }
        else 
        {
            tier1Propability = 35 - runeScore;
            if (tier1Propability < 0) tier1Propability = 0;
            tier2Propability = 35 - runeScore;
            if (tier2Propability < 0) tier2Propability = 0;
            tier3Propability = 30 + runeScore * 2;
            if (tier3Propability > 100) tier3Propability = 100;
        }

        RuneRarity runeRarity;
        int selectedRuneRarity = Random.Range(1, 101);
        if (selectedRuneRarity <= tier1Propability) runeRarity = RuneRarity.basic;
        else if (selectedRuneRarity <= tier2Propability) runeRarity = RuneRarity.refined;
        else runeRarity = RuneRarity.perfected;

        RuneObject rune = GiveRune(selectedRuneType, runeRarity);
        return rune;

    }
    
    private RuneObject GiveRune(Runes selectedRuneType, RuneRarity runeRarity)
    {
        if (selectedRuneType == Runes.spirit && runeRarity == RuneRarity.basic)
        {
            if (spiritRunesTier1.Length != 0)
            {
                int listLenght = spiritRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritRunesTier1[pickedIndex];
            }
            else return defaultRune;
        }
        if (selectedRuneType == Runes.spirit && runeRarity == RuneRarity.refined)
        {
            if (spiritRunesTier2.Length != 0)
            {
                int listLenght = spiritRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritRunesTier2[pickedIndex];
            }
            else return GiveRune(Runes.spirit, RuneRarity.basic);
        }
        if (selectedRuneType == Runes.spirit && runeRarity == RuneRarity.perfected)
        {
            if (spiritRunesTier3.Length != 0)
            {
                int listLenght = spiritRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritRunesTier3[pickedIndex];
            }
            else return GiveRune(Runes.spirit, RuneRarity.refined);
        }

        if (selectedRuneType == Runes.vitality && runeRarity == RuneRarity.basic)
        {
            if (vitalityRunesTier1.Length != 0)
            {
                int listLenght = vitalityRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityRunesTier1[pickedIndex];
            }
            else return defaultRune;
        }
        if (selectedRuneType == Runes.vitality && runeRarity == RuneRarity.refined)
        {
            if (vitalityRunesTier2.Length != 0)
            {
                int listLenght = vitalityRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityRunesTier2[pickedIndex];
            }
            else return GiveRune(Runes.vitality, RuneRarity.basic);
        }
        if (selectedRuneType == Runes.vitality && runeRarity == RuneRarity.perfected)
        {
            if (vitalityRunesTier3.Length != 0)
            {
                int listLenght = vitalityRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityRunesTier3[pickedIndex];
            }
            else return GiveRune(Runes.vitality, RuneRarity.refined);
        }

        if (selectedRuneType == Runes.agility && runeRarity == RuneRarity.basic)
        {
            if (agilityRunesTier1.Length != 0)
            {
                int listLenght = agilityRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return agilityRunesTier1[pickedIndex];
            }
            else return defaultRune;
        }
        if (selectedRuneType == Runes.agility && runeRarity == RuneRarity.refined)
        {
            if (agilityRunesTier2.Length != 0)
            {
                int listLenght = agilityRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return agilityRunesTier2[pickedIndex];
            }
            else return GiveRune(Runes.agility, RuneRarity.basic);
        }
        if (selectedRuneType == Runes.agility && runeRarity == RuneRarity.perfected)
        {
            if (agilityRunesTier3.Length != 0)
            {
                int listLenght = agilityRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return agilityRunesTier3[pickedIndex];
            }
            else return GiveRune(Runes.agility, RuneRarity.refined);
        }

        if (selectedRuneType == Runes.power && runeRarity == RuneRarity.basic)
        {
            if (powerRunesTier1.Length != 0)
            {
                int listLenght = powerRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return powerRunesTier1[pickedIndex];
            }
            else return defaultRune;
        }
        if (selectedRuneType == Runes.power && runeRarity == RuneRarity.refined)
        {
            if (powerRunesTier2.Length != 0)
            {
                int listLenght = powerRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return powerRunesTier2[pickedIndex];
            }
            else return GiveRune(Runes.power, RuneRarity.basic);
        }
        if (selectedRuneType == Runes.power && runeRarity == RuneRarity.perfected)
        {
            if (powerRunesTier3.Length != 0)
            {
                int listLenght = powerRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return powerRunesTier3[pickedIndex];
            }
            else return GiveRune(Runes.power, RuneRarity.refined);
        }

        if (selectedRuneType == Runes.spiritVitality && runeRarity == RuneRarity.basic)
        {
            return GiveRune(Runes.spiritVitality, RuneRarity.refined);
        }
        if (selectedRuneType == Runes.spiritVitality && runeRarity == RuneRarity.refined)
        {
            if (spiritVitalityRunesTier2.Length != 0)
            {
                int listLenght = spiritVitalityRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritVitalityRunesTier2[pickedIndex];
            }
            else
            {
                if (spiritScore > vitalityScore) return GiveRune(Runes.spirit, RuneRarity.refined);
                else return GiveRune(Runes.vitality, RuneRarity.refined);
            }
        }
        if (selectedRuneType == Runes.spiritVitality && runeRarity == RuneRarity.perfected)
        {
            if (spiritVitalityRunesTier3.Length != 0)
            {
                int listLenght = spiritVitalityRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritVitalityRunesTier3[pickedIndex];
            }
            else
            {
                if (spiritScore > vitalityScore) return GiveRune(Runes.spirit, RuneRarity.perfected);
                else return GiveRune(Runes.vitality, RuneRarity.perfected);
            }
        }

        if (selectedRuneType == Runes.spiritAgility && runeRarity == RuneRarity.basic)
        {
            return GiveRune(Runes.spiritAgility, RuneRarity.refined);
        }
        if (selectedRuneType == Runes.spiritAgility && runeRarity == RuneRarity.refined)
        {
            if (spiritAgilityRunesTier2.Length != 0)
            {
                int listLenght = spiritAgilityRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritAgilityRunesTier2[pickedIndex];
            }
            else
            {
                if (spiritScore > agilityScore) return GiveRune(Runes.spirit, RuneRarity.refined);
                else return GiveRune(Runes.agility, RuneRarity.refined);
            }
        }
        if (selectedRuneType == Runes.spiritAgility && runeRarity == RuneRarity.perfected)
        {
            if (spiritAgilityRunesTier3.Length != 0)
            {
                int listLenght = spiritAgilityRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritAgilityRunesTier3[pickedIndex];
            }
            else
            {
                if (spiritScore > agilityScore) return GiveRune(Runes.spirit, RuneRarity.perfected);
                else return GiveRune(Runes.agility, RuneRarity.perfected);
            }
        }

        if (selectedRuneType == Runes.spiritPower && runeRarity == RuneRarity.basic)
        {
            return GiveRune(Runes.spiritPower, RuneRarity.refined);
        }
        if (selectedRuneType == Runes.spiritPower && runeRarity == RuneRarity.refined)
        {
            if (spiritPowerRunesTier2.Length != 0)
            {
                int listLenght = spiritPowerRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritPowerRunesTier2[pickedIndex];
            }
            else
            {
                if (spiritScore > powerScore) return GiveRune(Runes.spirit, RuneRarity.refined);
                else return GiveRune(Runes.power, RuneRarity.refined);
            }
        }
        if (selectedRuneType == Runes.spiritPower && runeRarity == RuneRarity.perfected)
        {
            if (spiritPowerRunesTier3.Length != 0)
            {
                int listLenght = spiritPowerRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritPowerRunesTier3[pickedIndex];
            }
            else
            {
                if (spiritScore > powerScore) return GiveRune(Runes.spirit, RuneRarity.perfected);
                else return GiveRune(Runes.power, RuneRarity.perfected);
            }
        }

        if (selectedRuneType == Runes.vitalityAgility && runeRarity == RuneRarity.basic)
        {
            return GiveRune(Runes.vitalityAgility, RuneRarity.refined);
        }
        if (selectedRuneType == Runes.vitalityAgility && runeRarity == RuneRarity.refined)
        {
            if (vitalityAgilityRunesTier2.Length != 0)
            {
                int listLenght = vitalityAgilityRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityAgilityRunesTier2[pickedIndex];
            }
            else
            {
                if (vitalityScore > agilityScore) return GiveRune(Runes.vitality, RuneRarity.refined);
                else return GiveRune(Runes.agility, RuneRarity.refined);
            }
        }
        if (selectedRuneType == Runes.vitalityAgility && runeRarity == RuneRarity.perfected)
        {
            if (vitalityAgilityRunesTier3.Length != 0)
            {
                int listLenght = vitalityAgilityRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityAgilityRunesTier3[pickedIndex];
            }
            else
            {
                if (vitalityScore > agilityScore) return GiveRune(Runes.vitality, RuneRarity.perfected);
                else return GiveRune(Runes.agility, RuneRarity.perfected);
            }
        }

        if (selectedRuneType == Runes.vitalityPower && runeRarity == RuneRarity.basic)
        {
            return GiveRune(Runes.vitalityPower, RuneRarity.refined);
        }
        if (selectedRuneType == Runes.vitalityPower && runeRarity == RuneRarity.refined)
        {
            if (vitalityPowerRunesTier2.Length != 0)
            {
                int listLenght = vitalityPowerRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityPowerRunesTier2[pickedIndex];
            }
            else
            {
                if (vitalityScore > powerScore) return GiveRune(Runes.vitality, RuneRarity.refined);
                else return GiveRune(Runes.power, RuneRarity.refined);
            }
        }
        if (selectedRuneType == Runes.vitalityPower && runeRarity == RuneRarity.perfected)
        {
            if (vitalityPowerRunesTier3.Length != 0)
            {
                int listLenght = vitalityPowerRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityPowerRunesTier3[pickedIndex];
            }
            else
            {
                if (vitalityScore > powerScore) return GiveRune(Runes.vitality, RuneRarity.perfected);
                else return GiveRune(Runes.power, RuneRarity.perfected);
            }
        }

        if (selectedRuneType == Runes.agilityPower && runeRarity == RuneRarity.basic)
        {
            return GiveRune(Runes.agilityPower, RuneRarity.refined);
        }
        if (selectedRuneType == Runes.agilityPower && runeRarity == RuneRarity.refined)
        {
            if (agilityPowerRunesTier2.Length != 0)
            {
                int listLenght = agilityPowerRunesTier2.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return agilityPowerRunesTier2[pickedIndex];
            }
            else
            {
                if (agilityScore > powerScore) return GiveRune(Runes.agility, RuneRarity.refined);
                else return GiveRune(Runes.power, RuneRarity.refined);
            }
        }
        if (selectedRuneType == Runes.agilityPower && runeRarity == RuneRarity.perfected)
        {
            if (agilityPowerRunesTier3.Length != 0)
            {
                int listLenght = agilityPowerRunesTier3.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return agilityPowerRunesTier3[pickedIndex];
            }
            else
            {
                if (agilityScore > powerScore) return GiveRune(Runes.agility, RuneRarity.perfected);
                else return GiveRune(Runes.power, RuneRarity.perfected);
            }
        }
        else return defaultRune;
    }

    public void RandomizeNewRunes()
    {
        Debug.Log("Runes " + rune1 + " " + rune2 + " " + rune3);
        
        RuneObject rune1temp = GetRandomRune();
        rune1.GetComponent<SelectRuneButton>().SetNewRune(rune1temp);
        RuneObject rune2temp = GetRandomRune();
        while(rune1temp == rune2temp) 
            rune2temp = GetRandomRune();
        rune2.GetComponent<SelectRuneButton>().SetNewRune(rune2temp);
        RuneObject rune3temp = GetRandomRune();
        while (rune3temp == rune2temp || rune3temp == rune1temp || (rune3temp.runeType == rune1temp.runeType && rune3temp.runeType == rune2temp.runeType)) 
            rune3temp = GetRandomRune();
        rune3.GetComponent<SelectRuneButton>().SetNewRune(rune3temp);
    }
}
