using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour {

    public GameModel gameModel;
    //-------------------------
    //
    //  Entities
    //
    //-------------------------


    public Enemy enemy;
    public void EnemyAttackEvent()
    {
        if (gameModel.player != null)
            gameModel.player.StartDieAnimation();
        AudioManager.PlaySFX(enemy.audioSource, enemy.sfxHitClip);
    }

    public void PlayerDiedEvent()
    {
        if (gameModel != null)
            gameModel.ShowLoseMenu();
    }

    public void PlayerWinEvent()
    {
        if (gameModel != null)
            gameModel.ShowWinMenu();
    }
}
