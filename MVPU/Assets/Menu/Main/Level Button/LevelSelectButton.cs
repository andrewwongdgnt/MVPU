using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [Tooltip("The index that defines the order of the level regardless of world index. EG, if this is set to 0, then this level belongs to level 1 of world 1, or 2, etc...")]
    public int levelIndex;

    public Text label;

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
        UpdateDisplay();
    }
    LevelUtil.LevelID GetResolvedLevelId()
    {
        LevelUtil.LevelID[] levelIds = LevelUtil.WorldToLevelArr[levelIndex];

        int currentWorldIndex = LevelSelectUtil.GetCurrentWorld();
        if (levelIds.Length > currentWorldIndex)
            return levelIds[LevelSelectUtil.GetCurrentWorld()];
        else
            return LevelUtil.LevelID.NO_LEVEL;

    }
    public void UpdateDisplay()
    {
        LevelUtil.LevelID levelId = GetResolvedLevelId();

        if (levelId != LevelUtil.LevelID.NO_LEVEL && levelId != LevelUtil.LevelID.TEST_LEVEL)
        {
            Button btn = GetComponent<Button>();
            LevelUtil.LevelID[] levelPrereqs = LevelUtil.LevelPrereq.ContainsKey(levelId) ? LevelUtil.LevelPrereq[levelId] : new LevelUtil.LevelID[] { };

            btn.interactable = LevelSelectUtil.GetCurrentWorld() < LevelUtil.UNAVAILABLE_WORLD_START_INDEX && (LevelUtil.unlockFirst50Levels || levelPrereqs.Length == 0 || Array.Exists(levelPrereqs, l =>
                  SaveStateUtil.LoadLevel(l) != null
                ));

            if (!btn.interactable)
            {
                star1.sprite = star2.sprite = star3.sprite = starDisable;
            }
            else
            {
                SaveStateUtil.LevelState levelState = SaveStateUtil.LoadLevel(levelId);
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

    public void GoToLevel()
    {
        LevelUtil.LevelID levelId = GetResolvedLevelId();
        if (levelId != LevelUtil.LevelID.NO_LEVEL)
        {
            LevelUtil.levelToLoad = levelId;
            SceneManager.LoadScene("Load Screen");
        }
    }

}
