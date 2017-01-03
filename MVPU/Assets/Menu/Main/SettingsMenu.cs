using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Toggle tutorialToggle;
    public Slider entitySpeedMultiplier;
    void Start()
    {
        tutorialToggle.isOn = SettingsManager.IsTutorialOn();
        entitySpeedMultiplier.value = SettingsManager.GetEntitySpeedMultipler();
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

}
