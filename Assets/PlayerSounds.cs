using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    int prevRoll = 3;

    public void StepEvent()
    {
        int step;

        /*
        int dice = Random.Range(0, 3);

        if(dice == prevRoll && dice == 3)
        {
            dice = 0;
            prevRoll = dice;
        }
        else if(dice == prevRoll)
        {
            dice++;
            prevRoll = dice;
        }
        else
        {
            prevRoll = dice;
        }
        */
        if(prevRoll == 3)
        {
            step = 2;
            prevRoll = step;
        }
        else
        {
            step = 3;
            prevRoll = step;
        }

        switch (step)
        {
            case 0:
                SoundManager.PlaySound(SoundManager.Sound.Footstep1, transform.position);
                break;
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.Footstep2, transform.position);
                break;
            case 2:
                SoundManager.PlaySound(SoundManager.Sound.Footstep3, transform.position);
                break;
            case 3:
                SoundManager.PlaySound(SoundManager.Sound.Footstep4, transform.position);
                break;
        }
    }
}
