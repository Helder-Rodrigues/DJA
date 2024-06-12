using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1Manager : MonoBehaviour
{
    public static float highScore = 0;
    void Awake()
    {
        GameManager.CurrLvl = 0;
        GameManager.GhostMove = true;
    }
}
