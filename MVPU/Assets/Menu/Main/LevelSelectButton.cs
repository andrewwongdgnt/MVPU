using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour {

    public LevelManager.LevelID levelId;

    public Image star1;
    public Image star2;
    public Image star3;

    public Sprite starSpecial;
    public Sprite starOn;
    public Sprite starOff;
    public Sprite starDisable;

    // Use this for initialization
    void Start()
    {
        if (levelId != LevelManager.LevelID.TEST_LEVEL)
        {
            Button btn = GetComponent<Button>();
            LevelManager.LevelID[] levelPrereqs = LevelManager.LevelPrereq[levelId];

            btn.interactable = levelPrereqs.Length == 0 || Array.Exists(levelPrereqs, l =>
                  SaveStateManager.LoadLevel(l) != null
                )
                || false; // override this for testing purposes: True to unlock all levels, False for normal rules;

            if (!btn.interactable)
            {
                star1.sprite = star2.sprite = star3.sprite = starDisable;
            }
            else
            {
                SaveStateManager.LevelState levelState = SaveStateManager.LoadLevel(levelId);
                if (levelState == null)
                {
                    star1.sprite = star2.sprite = star3.sprite = starOff;
                }
                else
                {
                    ScoringModel.ScoreTypes scoreType = ScoringModel.GetResult(levelState.moveCount, levelId);
                    switch (scoreType)
                    {
                        case ScoringModel.ScoreTypes.MIN:
                            star1.sprite = star2.sprite = star3.sprite = starSpecial;
                            break;
                        case ScoringModel.ScoreTypes.GREAT:
                            star1.sprite = star2.sprite = star3.sprite = starOn;
                            break;
                        case ScoringModel.ScoreTypes.GOOD:
                            star1.sprite = star2.sprite = starOn;
                            star3.sprite = starOff;
                            break;
                        case ScoringModel.ScoreTypes.ADEQUATE:
                            star1.sprite = starOn;
                            star2.sprite = star3.sprite = starOff;
                            break;
                        case ScoringModel.ScoreTypes.NONE:
                            star1.sprite = star2.sprite = star3.sprite = starOff;
                            break;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToLevel()
    {
        LevelManager.levelToLoad = levelId;
        SceneManager.LoadScene("Load Screen");
    }
}
