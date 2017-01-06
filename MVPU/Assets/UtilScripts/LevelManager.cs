using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager {

    public static string levelToLoad = "Test Level";

    public static Dictionary<string, LevelScore> LevelScoreMap = new Dictionary<string, LevelScore>
        {
            { "Test Level", new LevelScore(10) },
            { "Level 1 - 1", new LevelScore(10) },
            { "Level 1 - 2", new LevelScore(8) },
            { "Level 1 - 3", new LevelScore(9) },
            { "Level 1 - 4", new LevelScore(9) },
            { "Level 1 - 5", new LevelScore(10) },
            { "Level 1 - 6", new LevelScore(9) },
            { "Level 1 - 7", new LevelScore(18) },
        };

    public static Dictionary<string, TutorialAction[]> TutorialContent = new Dictionary<string, TutorialAction[]>
        {
            { "Level 1 - 1", new TutorialAction[] {
                new TutorialAction("Hello!", TutorialAction.Action.NONE),
                new TutorialAction("My name is Chomp and I'm here to help you reunite the Monkey and Penguin.", TutorialAction.Action.NONE),
                new TutorialAction("Please swipe in any direction to move the Monkey.", TutorialAction.Action.SWIPE),
                new TutorialAction("Yay! Now keep going until the Monkey reaches the Penguin.", TutorialAction.Action.NONE) } },

            { "Level 1 - 2", new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("This is Kongo. He is grumpy and wants to attack the Monkey.", TutorialAction.Action.NONE),
                new TutorialAction("For every move the Monkey does, Kongo will try to follow in a direct path. Try it now.", TutorialAction.Action.SWIPE),
                new TutorialAction("Try to confuse Kongo by making him walk into the rocks.", TutorialAction.Action.NONE) } },
            { "Level 1 - 3", new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("Sometimes it is better to just not move at all and let Kongo walk into a trap.", TutorialAction.Action.NONE),
                new TutorialAction("Double tap on the screen to skip your move.", TutorialAction.Action.TAP),
                new TutorialAction("Good Job! Keep going!", TutorialAction.Action.NONE) } },
            { "Level 1 - 6", new TutorialAction[] {
                new TutorialAction("Hello again!", TutorialAction.Action.NONE),
                new TutorialAction("Looks like there are two Kongos, which means double the danger.", TutorialAction.Action.NONE),
                new TutorialAction("But not to worry! There is a way to still get the Monkey to the Penguin.", TutorialAction.Action.NONE),
                new TutorialAction("If you can get one Kongo to run into the other, only one will come out. Give it a try!", TutorialAction.Action.NONE) } },
            { "Level 1 - 7", new TutorialAction[] {
                new TutorialAction("Uh Oh! Kongo looks really mad.", TutorialAction.Action.NONE),
                new TutorialAction("When Kongo is red, he can move two steps everytime the Monkey moves one.", TutorialAction.Action.NONE),
                new TutorialAction("So be extra careful with your moves. Good luck!", TutorialAction.Action.NONE) } }
        };


}
