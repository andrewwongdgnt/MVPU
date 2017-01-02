using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public Animator anim;
    public Button tutorialButton;
    public Text tutorialText;
    public Text tutorial2Text;

    private int tutorialIndex = -1;
    private TutorialAction[] _tutorialActionArr;
    public TutorialAction[] tutorialActionArr
    {
        set
        {
            _tutorialActionArr = value;
        }
    }

    private Boolean tutorialEnded;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SetObjectsActiveStatusAfterDelay());
    }

    IEnumerator SetObjectsActiveStatusAfterDelay()
    {
        yield return 0;
        tutorialEnded = _tutorialActionArr == null;
        tutorialButton.gameObject.SetActive(!tutorialEnded);
        ShowTutorialMascot(!tutorialEnded);

    }

    private void ShowTutorialMascot(Boolean show)
    {
        Array.ForEach(gameObject.GetComponentsInChildren<Image>(), im =>
        {
            Color tmp = im.color;
            tmp.a = show ? 1 : 0;
            im.color = tmp;
        });
    }


    // Update is called once per frame
    void Update()
    {
        if (tutorialEnded)
        {
            tutorialButton.gameObject.SetActive(false);
        }

    }

    public TutorialAction.Action AdvanceTutorial(Boolean forceAdvance = false)
    {

        if (_tutorialActionArr != null)
        {

            ShowTutorialMascot(true);
            if (forceAdvance || tutorialIndex < 0 || _tutorialActionArr.Length > tutorialIndex && _tutorialActionArr[tutorialIndex].action == TutorialAction.Action.NONE)
            {
                tutorialIndex++;
                if (tutorialIndex < 0)
                {
                    tutorialIndex = 0;
                }

                if (_tutorialActionArr.Length > tutorialIndex)
                {
                    tutorialButton.gameObject.SetActive(true);
                    tutorialText.text = _tutorialActionArr[tutorialIndex].text;
                    TutorialAction.Action action = _tutorialActionArr[tutorialIndex].action;
                    tutorial2Text.text = action == TutorialAction.Action.NONE ? "Tap here to continue" : "Follow tutorial instructions";
                    return action;
                }
                else
                {
                    tutorialButton.gameObject.SetActive(false);
                    anim.SetBool("EndTutorial", true);
                    tutorialEnded = true;
                    return TutorialAction.Action.ALL;
                }
            }
            else
            {
                return _tutorialActionArr.Length > tutorialIndex ? _tutorialActionArr[tutorialIndex].action : TutorialAction.Action.ALL;
            }
        }
        else
        {
            tutorialButton.gameObject.SetActive(false);
            ShowTutorialMascot(false);
            return TutorialAction.Action.ALL;
        }
    }
}
