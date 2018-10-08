using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour {

    public GameModel gameModel { private get; set; }

    //Attacker Section
    public IAttacker attacker { private get; set; }
    public void AttackerAttackEvent()
    {
        gameModel.player.StartDieAnimation(attacker.GetPlayerLoseAnimationName());
        AudioUtil.PlaySFX(attacker.audioSource, attacker.sfxHitClip);
    }

    //Player Section
    public void PlayerDiedEvent()
    {
        gameModel.ShowLoseMenu();
    }

    public void PlayerWinEvent()
    {
        gameModel.ShowWinMenu();
    }

    //Walker Section
    public IWalker walker { private get; set; }
    public void WalkerFootStepEvent()
    {
        LevelUtil.LevelType levelType = gameModel.currentLevelType;
        AudioClip sfxFootStep = walker.GetSfxFootStep(levelType);

        AudioUtil.PlaySFX(walker.audioSource, sfxFootStep);
    }
}
