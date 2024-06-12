using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using TMPro;

public static class GameManager
{
    private static int currLvl = 0;
    public static int CurrLvl
    {
        set { if (value >= 0 && value <= 2) { currLvl = value; } }
        get { return currLvl; }
    }
    public static int lvlsUnlocked = 1;


    #region Attributes
    public static int playerPoints = 0;
    
    public static int speedLvl = 1;
    public static float speedPerc = 0;
    public readonly static int speedValue = 15;
    public static int speedPrice = 4;

    public static int visionLvl = 1;
    public static float visionPerc = 0;
    public readonly static int visionValue = 5;
    public static int visionPrice = 3;

    public static int pointsLvl = 1;
    public static float pointsPerc = 0;
    public readonly static int pointsValue = 5;
    public static int pointsPrice = 1;
    #endregion

    public static bool GhostMove;

    public static bool gamePaused = false;

    public static bool GameLoaded = false;

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("lvlsUnlocked", lvlsUnlocked);

        PlayerPrefs.SetInt("playerPoints", playerPoints);

        PlayerPrefs.SetInt("speedLvl", speedLvl);
        PlayerPrefs.SetFloat("speedPerc", speedPerc);
        PlayerPrefs.SetInt("speedPrice", speedPrice);

        PlayerPrefs.SetInt("visionLvl", visionLvl);
        PlayerPrefs.SetFloat("visionPerc", visionPerc);
        PlayerPrefs.SetInt("visionPrice", visionPrice);

        PlayerPrefs.SetInt("pointsLvl", pointsLvl);
        PlayerPrefs.SetFloat("pointsPerc", pointsPerc);
        PlayerPrefs.SetInt("pointsPrice", pointsPrice);

        PlayerPrefs.SetFloat("Lvl1Manager.highScore", Lvl1Manager.highScore);
        PlayerPrefs.SetFloat("Lvl2Manager.highScore", Lvl2Manager.highScore);
        PlayerPrefs.SetFloat("Lvl3Manager.highScore", Lvl3Manager.highScore);
    }
    public static void LoadGame()
    {
        lvlsUnlocked = PlayerPrefs.GetInt("lvlsUnlocked");

        playerPoints = PlayerPrefs.GetInt("playerPoints");

        speedLvl = PlayerPrefs.GetInt("speedLvl");
        speedPerc = PlayerPrefs.GetFloat("speedPerc");
        speedPrice = PlayerPrefs.GetInt("speedPrice");

        visionLvl = PlayerPrefs.GetInt("visionLvl");
        visionPerc = PlayerPrefs.GetFloat("visionPerc");
        visionPrice = PlayerPrefs.GetInt("visionPrice");

        pointsLvl = PlayerPrefs.GetInt("pointsLvl");
        pointsPerc = PlayerPrefs.GetFloat("pointsPerc");
        pointsPrice = PlayerPrefs.GetInt("pointsPrice");

        Lvl1Manager.highScore = PlayerPrefs.GetFloat("Lvl1Manager.highScore");
        Lvl2Manager.highScore = PlayerPrefs.GetFloat("Lvl2Manager.highScore");
        Lvl3Manager.highScore = PlayerPrefs.GetFloat("Lvl3Manager.highScore");
    }
}
