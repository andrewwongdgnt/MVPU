using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpIndicatorMenu : MonoBehaviour
{
    public RectTransform content;
    public RectTransform textContent;

    private const string REPLAY = "Replay levels to relearn how to play";
    private const string PLAY = "Play levels to learn how to play";

    void Start()
    {
        Text text = textContent.GetComponent<Text>();
        List<string> tutorialContent = getTutorialContent();
        string instructions = tutorialContent.Count == 0 ? PLAY : REPLAY;
        text.text = instructions + "\n" + string.Join("\n", tutorialContent.ToArray());
        float height =  text.preferredHeight;

        content.sizeDelta = new Vector2(0, height + -text.rectTransform.anchoredPosition.y*2);

    }

    private List<string> getTutorialContent()
    {
        List<string> content = new List<string>();
        foreach (LevelUtil.LevelID levelId in LevelUtil.TutorialContent.Keys)
        {
            if (SaveStateUtil.LoadLevel(levelId) != null && LevelUtil.TutorialContent[levelId].first != null)
            {
                content.Add(LevelUtil.TutorialContent[levelId].first);
            }

        }
        return content;
    }
}
