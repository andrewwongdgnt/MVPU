using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour {

    public GameModel gameModel { private get; set; }
    //-------------------------
    //
    //  Entities
    //
    //-------------------------


    public Enemy enemy;
    public void EnemyAttackEvent()
    {
        gameModel.player.StartDieAnimation(enemy.GetPlayerLoseAnimationName());
        AudioUtil.PlaySFX(enemy.audioSource, enemy.sfxHitClip);
    }

    public void PlayerDiedEvent()
    {
        gameModel.ShowLoseMenu();
    }

    public void PlayerWinEvent()
    {
        gameModel.ShowWinMenu();
    }
}
