using UnityEngine;
using System.Collections;
using SimpleJSON;

public class SaveStateManager  {

    static string MOVE_COUNT_KEY = "moveCount";

	public static void SaveLevel (string levelId, int moveCount) {

        string jsonString = PlayerPrefs.GetString(levelId);
        JSONNode savedNode = JSON.Parse(jsonString);

        int moveCountToUse = moveCount;
        if (savedNode != null)
        {
            int savedMoveCount = savedNode[MOVE_COUNT_KEY].AsInt;
            moveCountToUse = Mathf.Min(savedMoveCount, moveCount);
        }

        JSONNode node = new JSONClass();
        node[MOVE_COUNT_KEY].AsInt = moveCountToUse;

        PlayerPrefs.SetString(levelId, node.ToString());
    }

    public static LevelState LoadLevel(string levelId)
    {
        string jsonString = PlayerPrefs.GetString(levelId);
        JSONNode node = JSON.Parse(jsonString);

        if (node != null)
        {
            LevelState levelState = new LevelState();
            levelState.moveCount = node[MOVE_COUNT_KEY].AsInt;

            return levelState;
        }
        return null;
    }

    public class LevelState
    {
        public int moveCount;
    }

}
