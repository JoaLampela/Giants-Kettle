using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{

    public void OnHover()
    {
        SoundManager.PlayUISound(SoundManager.Sound.ButtonHover);
    }

    public void OnClick()
    {
        SoundManager.PlayUISound(SoundManager.Sound.ButtonClick);
    }
}
