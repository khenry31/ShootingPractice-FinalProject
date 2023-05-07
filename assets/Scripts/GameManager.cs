using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static int score;
    private static bool firstTargetDown = false;
    private static int dialogueCount;
    private static bool testStarted = false;
    private static bool testEnded = false;
    private static bool timerReset = false;


    public static int Score
    {
        get { return score; }
        set { score = value; }
    }

    public static bool FirstTargetDown
    {
        get { return firstTargetDown; }
        set { firstTargetDown = value; }
    }

    public static int DialogueCount
    {
        get { return dialogueCount; }
        set { dialogueCount = value; }
    }

    public static bool TestStarted
    {
        get { return testStarted; }
        set { testStarted = value; }
    }

    public static bool TestEnded
    {
        get { return testEnded; }
        set { testEnded = value; }
    }

    public static bool TimerReset
    {
        get { return timerReset; }
        set { timerReset = value; }
    }


    private void Awake()
    {
        instance = this;
    }

    public static void openSecondDoor()
    {
        //start the next dialogue then open the second door
        Debug.Log("second door opened");
    }

}
