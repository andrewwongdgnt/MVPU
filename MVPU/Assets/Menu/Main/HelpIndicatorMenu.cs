using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpIndicatorMenu : MonoBehaviour
{
    public RectTransform content;
    public RectTransform textContent;

    void Start()
    {
        Text text = textContent.GetComponent<Text>();
        text.text = text.text +"\n"+populateTextContent();
        float height =  text.preferredHeight;

        content.sizeDelta = new Vector2(content.rect.width, height + -text.rectTransform.anchoredPosition.y*2);

    }

    private string populateTextContent()
    {
        List<string> content = new List<string>();
        foreach (LevelUtil.LevelID levelId in LevelUtil.TutorialContent.Keys)
        {
            if (SaveStateUtil.LoadLevel(levelId) != null && LevelUtil.TutorialContent[levelId].first != null)
            {
                content.Add(LevelUtil.TutorialContent[levelId].first);
            }

        }
        return string.Join("\n", content.ToArray()); ;
    }
}
