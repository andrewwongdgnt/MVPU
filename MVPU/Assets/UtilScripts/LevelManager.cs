using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager {

    public enum LevelID
    {
        TEST_LEVEL
            , LEVEL_1_1, LEVEL_1_2, LEVEL_1_3, LEVEL_1_4, LEVEL_1_5, LEVEL_1_6, LEVEL_1_7, LEVEL_1_8, LEVEL_1_9, LEVEL_1_10
            , LEVEL_2_1, LEVEL_2_2, LEVEL_2_3, LEVEL_2_4, LEVEL_2_5, LEVEL_2_6, LEVEL_2_7, LEVEL_2_8, LEVEL_2_9, LEVEL_2_10

    }

    public static LevelID levelToLoad = LevelID.TEST_LEVEL;

    public static Dictionary<LevelID, LevelScore> LevelScoreMap = new Dictionary<LevelID, LevelScore>
        {
            { LevelID.TEST_LEVEL, new LevelScore(10) },
            { LevelID.LEVEL_1_1, new LevelScore(10) },
            { LevelID.LEVEL_1_2, new LevelScore(8) },
            { LevelID.LEVEL_1_3, new LevelScore(9) },
            { LevelID.LEVEL_1_4, new LevelScore(9) },
            { LevelID.LEVEL_1_5, new LevelScore(10) },
            { LevelID.LEVEL_1_6, new LevelScore(9) },
            { LevelID.LEVEL_1_7, new LevelScore(18) },
        };

    public static Dictionary<LevelID, TutorialAction[]> TutorialContent = new Dictionary<LevelID, TutorialAction[]>
        {
            { LevelID.LEVEL_1_1, new TutorialAction[] {
                new TutorialAction("Hello!", TutorialAction.Action.NONE),
                new TutorialAction("My name is Chomp and I'm here to help you reunite the Monkey and Penguin.", TutorialAction.Action.NONE),
                new TutorialAction("Please swipe in any direction to move the Monkey.", TutorialAction.Action.SWIPE),
                new TutorialAction("Yay! Now keep going until the Monkey reaches the Penguin.", TutorialAction.Action.NONE) } },

            { LevelID.LEVEL_1_2, new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("This is Kongo. He is grumpy and wants to attack the Monkey.", TutorialAction.Action.NONE),
                new TutorialAction("For every move the Monkey does, Kongo will try to follow in a direct path. Try it now.", TutorialAction.Action.SWIPE),
                new TutorialAction("Try to confuse Kongo by making him walk into the rocks.", TutorialAction.Action.NONE) } },
            { LevelID.LEVEL_1_3, new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("Sometimes it is better to just not move at all and let Kongo walk into a trap.", TutorialAction.Action.NONE),
                new TutorialAction("Double tap on the screen to skip your move.", TutorialAction.Action.TAP),
                new TutorialAction("Good Job! Keep going!", TutorialAction.Action.NONE) } },
            { LevelID.LEVEL_1_6, new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("Looks like there are two Kongos, which means double the danger.", TutorialAction.Action.NONE),
                new TutorialAction("But not to worry! There is a way to still get the Monkey to the Penguin.", TutorialAction.Action.NONE),
                new TutorialAction("If you can get one Kongo to run into the other, only one will come out. Give it a try!", TutorialAction.Action.NONE) } },
            { LevelID.LEVEL_1_7, new TutorialAction[] {
                new TutorialAction("Uh Oh! Kongo looks really mad.", TutorialAction.Action.NONE),
                new TutorialAction("When Kongo is red, he can move two steps everytime the Monkey moves one.", TutorialAction.Action.NONE),
                new TutorialAction("So be extra careful with your moves. Good luck!", TutorialAction.Action.NONE) } }
        };


}
