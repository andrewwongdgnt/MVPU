using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager  {

    public static void PlaySFX(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;
        audioSource.volume = SettingsManager.GetSFXVolume() / 100;
        audioSource.loop = false;
        PlayAudio(audioSource, audioClip);
    }

    public static void PlayMusic(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;
        if (audioClip == audioSource.clip)
            return;
        audioSource.volume = SettingsManager.GetMusicVolume() / 100;
        audioSource.loop = true;
        PlayAudio(audioSource, audioClip);
    }

    private static void PlayAudio(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
