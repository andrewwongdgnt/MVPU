using UnityEngine;
using System.Collections;

public class SettingsManager
{

    private readonly static string TUTORIAL_FLAG_KEY = "TutorialFlagKey";

    public static void SetTutorial(bool value)
    {
        PlayerPrefs.SetInt(TUTORIAL_FLAG_KEY, value ? 1 : 0);
    }
    public static bool IsTutorialOn()
    {
        return PlayerPrefs.HasKey(TUTORIAL_FLAG_KEY) ? PlayerPrefs.GetInt(TUTORIAL_FLAG_KEY) == 1 : true;
    }

    private readonly static string ENTITY_SPEED_MULTIPLIER_KEY = "EntitySpeedMultiplierKey";

    public static void SetEntitySpeedMultiplier(float value)
    {
        PlayerPrefs.SetFloat(ENTITY_SPEED_MULTIPLIER_KEY, value);
    }
    public static float GetEntitySpeedMultipler()
    {
        return PlayerPrefs.HasKey(ENTITY_SPEED_MULTIPLIER_KEY) ? PlayerPrefs.GetFloat(ENTITY_SPEED_MULTIPLIER_KEY) : 1;
    }
}
