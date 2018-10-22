using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Toggle tutorialToggle;
    public Slider entitySpeedMultiplier;
    public Slider musicVolume;
    public Slider sfxVolume;
    public AudioSource musicAudioSource;
    void Start()
    {
        tutorialToggle.isOn = SettingsUtil.IsTutorialOn();
        entitySpeedMultiplier.value = SettingsUtil.GetEntitySpeedMultipler();
        musicVolume.value = SettingsUtil.GetMusicVolume();
        sfxVolume.value = SettingsUtil.GetSFXVolume();
    }

	public void ToggleTutorial (bool value) {
        Debug.Log("Tutorial is "+ (value ? "on" : "off"));
        SettingsUtil.SetTutorial(value);
    }

    public void EntitySpeed (float speed)
    {
        Debug.Log("Entity Speed is " + (speed));
        SettingsUtil.SetEntitySpeedMultiplier(speed);
    }

    public void MusicVolume(float value)
    {
        Debug.Log("Music volume is " + (value));
        SettingsUtil.SetMusicVolume(value);
        musicAudioSource.volume = value / 100;
    }

    public void SFXVolume(float value)
    {
        Debug.Log("SFX volume is " + (value));
        SettingsUtil.SetSFXVolume(value);
    }

}
