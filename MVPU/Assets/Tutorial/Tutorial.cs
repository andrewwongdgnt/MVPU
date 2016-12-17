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

    private int tutorialIndex =-1;
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
        tutorialEnded = false;
        tutorialButton.gameObject.SetActive(_tutorialActionArr != null);
        gameObject.SetActive(_tutorialActionArr != null);

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

            gameObject.SetActive(true);
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
                    return _tutorialActionArr[tutorialIndex].action;
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
            gameObject.SetActive(false);
            return TutorialAction.Action.ALL;
        }
    }
}
