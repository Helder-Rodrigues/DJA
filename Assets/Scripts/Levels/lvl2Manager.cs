using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Manager : MonoBehaviour
{
    public static float highScore = 0;
    void Awake()
    {
        GameManager.CurrLvl = 1;
    }
}
