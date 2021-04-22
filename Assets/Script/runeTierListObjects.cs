using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runeTierListObjects : MonoBehaviour
{
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
    public int vitalityScore = 2;
    public int agilityScore = 3;
    public int powerScore = 4;

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
        }
        return 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GetRandomRune();
        
    }

    public RuneObject GetRandomRune()
    {
        int upperLimit = (1 + spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore + spiritAgilityScore + spiritPowerScore + vitalityAgilityScore + vitalityPowerScore + agilityPowerScore);
        int selectedRuneNumber = Random.Range(1, upperLimit);
        Debug.Log(upperLimit + " " + selectedRuneNumber);
        Runes selectedRuneType = Runes.empty;
        if (selectedRuneNumber <= spiritScore) selectedRuneType = Runes.spirit;
        else if (selectedRuneNumber <= spiritScore + vitalityScore) selectedRuneType = Runes.vitality;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore) selectedRuneType = Runes.agility;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore) selectedRuneType = Runes.power;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore) selectedRuneType = Runes.spiritVitality;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore + spiritAgilityScore) selectedRuneType = Runes.spiritAgility;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore + spiritAgilityScore + spiritPowerScore) selectedRuneType = Runes.spiritPower;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore + spiritAgilityScore + spiritPowerScore + vitalityAgilityScore) selectedRuneType = Runes.vitalityAgility;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore + spiritAgilityScore + spiritPowerScore + vitalityAgilityScore + vitalityPowerScore) selectedRuneType = Runes.vitalityPower;
        else if (selectedRuneNumber <= spiritScore + vitalityScore + agilityScore + powerScore + spiritVitalityScore + spiritAgilityScore + spiritPowerScore + vitalityAgilityScore + vitalityPowerScore + agilityPowerScore) selectedRuneType = Runes.agilityPower;


        Debug.Log(selectedRuneType);

        int runeScore = GetScore(selectedRuneType);
        int tier1Propability = 0;
        int tier2Propability = 0;
        int tier3Propability = 0;

        if(runeScore < 5)
        {
            tier1Propability = 100;
            tier2Propability = 0;
            tier3Propability = 0;
        }
        else if(runeScore < 10)
        {
            tier1Propability = 50 - runeScore * 5;
            tier2Propability = 50 + runeScore * 5;
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

        return GiveRune(selectedRuneType, runeRarity);

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
            if (spiritRunesTier1.Length != 0)
            {
                int listLenght = spiritRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritRunesTier1[pickedIndex];
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
            if (spiritVitalityRunesTier1.Length != 0)
            {
                int listLenght = spiritVitalityRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritVitalityRunesTier1[pickedIndex];
            }
            else
            {
                if (spiritScore > vitalityScore) return GiveRune(Runes.spirit, RuneRarity.basic);
                else return GiveRune(Runes.vitality, RuneRarity.basic);
            }
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
            if (spiritAgilityRunesTier1.Length != 0)
            {
                int listLenght = spiritAgilityRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritAgilityRunesTier1[pickedIndex];
            }
            else
            {
                if (spiritScore > agilityScore) return GiveRune(Runes.spirit, RuneRarity.basic);
                else return GiveRune(Runes.agility, RuneRarity.basic);
            }
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
            if (spiritPowerRunesTier1.Length != 0)
            {
                int listLenght = spiritPowerRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return spiritPowerRunesTier1[pickedIndex];
            }
            else
            {
                if (spiritScore > powerScore) return GiveRune(Runes.spirit, RuneRarity.basic);
                else return GiveRune(Runes.power, RuneRarity.basic);
            }
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
            if (vitalityAgilityRunesTier1.Length != 0)
            {
                int listLenght = vitalityAgilityRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityAgilityRunesTier1[pickedIndex];
            }
            else
            {
                if (vitalityScore > agilityScore) return GiveRune(Runes.vitality, RuneRarity.basic);
                else return GiveRune(Runes.agility, RuneRarity.basic);
            }
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
            if (vitalityPowerRunesTier1.Length != 0)
            {
                int listLenght = vitalityPowerRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return vitalityPowerRunesTier1[pickedIndex];
            }
            else
            {
                if (vitalityScore > powerScore) return GiveRune(Runes.vitality, RuneRarity.basic);
                else return GiveRune(Runes.power, RuneRarity.basic);
            }
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
            if (agilityPowerRunesTier1.Length != 0)
            {
                int listLenght = agilityPowerRunesTier1.Length;
                int pickedIndex = Random.Range(0, listLenght);
                return agilityPowerRunesTier1[pickedIndex];
            }
            else
            {
                if (agilityScore > powerScore) return GiveRune(Runes.agility, RuneRarity.basic);
                else return GiveRune(Runes.power, RuneRarity.basic);
            }
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
}
