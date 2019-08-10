using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip sfx;

    public void PlayScrollClick(float value)
    {
        AudioUtil.PlaySFX(audioSource, sfx, false);
    }

    public void PlayButtonClick()
    {
        AudioUtil.PlaySFX(audioSource, sfx);
    }
}
