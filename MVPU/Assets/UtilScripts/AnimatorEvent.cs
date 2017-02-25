using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour {

    //-------------------------
    //
    //  Entities
    //
    //-------------------------
    public Player player;
    public GameModel gameModel;

    public void EnemyAttackEvent()
    {
        if (player!=null)
            player.StartDieAnimation();
    }

    public void PlayerDiedEvent()
    {
        if (gameModel != null)
            gameModel.ShowLoseMenu();
    }
}
