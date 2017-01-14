using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour {

    public LevelManager.LevelID levelId;

    // Use this for initialization
    void Start()
    { 
        Button btn = GetComponent<Button>();
        LevelManager.LevelID[] levelPrereqs = LevelManager.LevelPrereq[levelId];

        btn.interactable = levelPrereqs.Length == 0 || Array.Exists(levelPrereqs, l =>
              SaveStateManager.LoadLevel(l) != null
            )
            || true; // override this for testing purposes: True to unlock all levels, False for normal rules;
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
