using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Manager : MonoBehaviour
{
    public static float highScore = 0;
    void Awake()
    {
        GameManager.CurrLvl = 2;
        GameManager.GhostMove = false;
    }
}
