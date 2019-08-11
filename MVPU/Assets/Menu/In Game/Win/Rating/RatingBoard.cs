using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingBoard : MonoBehaviour
{
    public Star star1;
    public Star star2;
    public Star star3;

    void Start()
    {

        star1.animatorEvent.genericAudioSource = star1.audioSource;
        star2.animatorEvent.genericAudioSource = star2.audioSource;
        star3.animatorEvent.genericAudioSource = star3.audioSource;
    }

    public void Display(ScoringModel.ScoreTypes scoreType)
    {

        StartCoroutine(DisplayInternal(scoreType));
    }

    private IEnumerator DisplayInternal(ScoringModel.ScoreTypes scoreType)
    {
        Animator star1Animator = star1.anim;
        Animator star2Animator = star2.anim;
        Animator star3Animator = star3.anim;

        star1Animator.SetBool("StarOff", true);
        star2Animator.SetBool("StarOff", true);
        star3Animator.SetBool("StarOff", true);
        star1Animator.SetBool("StarOff", false);
        star2Animator.SetBool("StarOff", false);
        star3Animator.SetBool("StarOff", false);

        if (scoreType != ScoringModel.ScoreTypes.NONE)
        {
            if (scoreType == ScoringModel.ScoreTypes.ADEQUATE || scoreType == ScoringModel.ScoreTypes.GOOD || scoreType == ScoringModel.ScoreTypes.GREAT || scoreType == ScoringModel.ScoreTypes.MIN)
            {
                yield return new WaitForSecondsRealtime(.5f);
                star1.animatorEvent.genericAudioClip = star1.starOnAudioClip;
                star1Animator.SetBool("StarOn", true);
            }
            if (scoreType == ScoringModel.ScoreTypes.GOOD || scoreType == ScoringModel.ScoreTypes.GREAT || scoreType == ScoringModel.ScoreTypes.MIN)
            {
                yield return new WaitForSecondsRealtime(.5f);
                star2.animatorEvent.genericAudioClip = star2.starOnAudioClip;
                star2Animator.SetBool("StarOn", true);
            }
            if (scoreType == ScoringModel.ScoreTypes.GREAT || scoreType == ScoringModel.ScoreTypes.MIN)
            {
                yield return new WaitForSecondsRealtime(.5f);
                star3.animatorEvent.genericAudioClip = star3.starOnAudioClip;
                star3Animator.SetBool("StarOn", true);
            }
            if (scoreType == ScoringModel.ScoreTypes.MIN)
            {
                yield return new WaitForSecondsRealtime(.5f);
                star1.animatorEvent.genericAudioClip = star1.starSpecialAudioClip;
                star2.animatorEvent.genericAudioClip = null;
                star3.animatorEvent.genericAudioClip = null;

                star1Animator.SetBool("StarSpecial", true);
                star2Animator.SetBool("StarSpecial", true);
                star3Animator.SetBool("StarSpecial", true);
            }
        }
    }
}
