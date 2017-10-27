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
    public InGameHelp inGameHelp;

    private int tutorialIndex = -1;
    private TutorialAction[] _tutorialActionArr;

    public void Init(TutorialAction[] tutorialActionArr)
    {
        _tutorialActionArr = tutorialActionArr;
        bool tutorialEnded = _tutorialActionArr == null;
        tutorialButton.gameObject.SetActive(!tutorialEnded);
        ShowTutorialMascot(!tutorialEnded);

    }

    private void ShowTutorialMascot(bool show)
    {
        Array.ForEach(gameObject.GetComponentsInChildren<Image>(), im =>
        {
            Color tmp = im.color;
            tmp.a = show ? 1 : 0;
            im.color = tmp;
        });
    }

    public TutorialAction.Action AdvanceTutorial(Boolean forceAdvance = false)
    {
        
        TutorialAction.Action action =  AdvanceTutorialInternal(forceAdvance);
        if (action == TutorialAction.Action.SWIPE)
            inGameHelp.Swipe();
        else if (action == TutorialAction.Action.TAP)
            inGameHelp.Tap();
        else if (action == TutorialAction.Action.HENEMY)
            inGameHelp.HEnemy();
        else if (action == TutorialAction.Action.VENEMY)
            inGameHelp.VEnemy();
        else 
            inGameHelp.Stop();
        return action;
    }
    private TutorialAction.Action AdvanceTutorialInternal(Boolean forceAdvance = false)
    {

        if (_tutorialActionArr != null)
        {

            ShowTutorialMascot(true);
            if (forceAdvance || tutorialIndex < 0 || _tutorialActionArr.Length > tutorialIndex && TutorialAction.isNoAction(_tutorialActionArr[tutorialIndex].action))
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
                    tutorial2Text.text = TutorialAction.isNoAction(action) ? "Tap here to continue" : "Follow tutorial instructions";
                    return action;
                }
                else
                {
                    tutorialButton.gameObject.SetActive(false);
                    anim.SetBool("EndTutorial", true);
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
