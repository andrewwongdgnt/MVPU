using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Toggle tutorialToggle;
    public Slider entitySpeedMultiplier;
    public Slider musicVolume;
    public Slider sfxVolume;
    void Start()
    {
        tutorialToggle.isOn = SettingsManager.IsTutorialOn();
        entitySpeedMultiplier.value = SettingsManager.GetEntitySpeedMultipler();
        musicVolume.value = SettingsManager.GetMusicVolume();
        sfxVolume.value = SettingsManager.GetSFXVolume();
    }

	public void ToggleTutorial (bool value) {
        Debug.Log("Tutorial is "+ (value ? "on" : "off"));
        SettingsManager.SetTutorial(value);
    }

    public void EntitySpeed (float speed)
    {
        Debug.Log("Entity Speed is " + (speed));
        SettingsManager.SetEntitySpeedMultiplier(speed);
    }

    public void MusicVolume(float value)
    {
        Debug.Log("Music volume is " + (value));
        SettingsManager.SetMusicVolume(value);
    }

    public void SFXVolume(float value)
    {
        Debug.Log("SFX volume is " + (value));
        SettingsManager.SetSFXVolume(value);
    }

}
