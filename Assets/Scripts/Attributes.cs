using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public static class Attributes
{
    private static int currLvl = 0;
    public static int CurrLvl
    {
        set { if (value >= 0 && value <= 2) { currLvl = value; } }
        get { return currLvl; }
    }

    public static int points = 0;

    public static int speed = 5;
    public static int jump = 2;
}
