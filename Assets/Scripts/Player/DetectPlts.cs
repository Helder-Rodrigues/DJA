using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlts : MonoBehaviour
{
    private int comboQuant;
    private int currComboQuant = 0;

    private string comboLvl = "ROYGBPUPI";
    private string combo = "";

    private bool lvlDone = false;

    private List<int> platesAlreadyDone = new List<int>();

    [SerializeField] private Score_Timer timer;

    private void Start()
    {
        GameManager.gamePaused = false;

        if (GameManager.CurrLvl == 0)
            comboQuant = 5;
        else if (GameManager.CurrLvl == 1)
            comboQuant = 1;
        else if (GameManager.CurrLvl == 2)
            comboQuant = 7;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAllUpper(other.name) && !lvlDone && !platesAlreadyDone.Contains(other.gameObject.GetInstanceID()))
        {
            AddIdToList(other);

            string color = other.name.ToUpper();

            char correctColor = comboLvl[combo.Length % comboLvl.Length];

            bool erased = false;
            if (color[0] == correctColor)
            {
                if (color[0] == 'P')
                    if (color[1] == comboLvl[combo.Length % comboLvl.Length + 1])
                        combo += color[0].ToString() + color[1].ToString();
                    else
                    {
                        RemoveLastCombo();
                        erased = true;
                    }
                else
                    combo += color[0];
            }
            else
            {
                RemoveLastCombo();
                erased = true;
            }

            if (!erased && combo.Length % comboLvl.Length == 0)
                currComboQuant++;

            if (currComboQuant == comboQuant)
            {
                lvlDone = true;
                if (GameManager.CurrLvl == 1)
                    LvlFinished();
            }
        }
        else if ((other.name.ToLower() == "meta" || other.name.ToLower() == "fim") && lvlDone)
        {
            LvlFinished();
        }
        else if (other.name.ToLower().Contains("ghost"))
        {
            if (other.name.ToLower().Contains("movetrigger"))
                GameManager.GhostMove = true;
            else
                GoToMenu();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            LvlFinished();
    }
    private void LvlFinished()
    {
        Score_Timer.stopTimer = true;
        GameManager.gamePaused = true;
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(2); //Lvls Scene
    }

    private void AddIdToList(Collider other)
    {
        int instanceID = other.gameObject.GetInstanceID();
        platesAlreadyDone.Add(instanceID);

        other.gameObject.GetComponent<Renderer>().material.color *= 0.5f;
    }

    private void RemoveLastCombo()
    {
        int lastComboQuant = combo.Length % comboLvl.Length;

        combo = combo.Substring(0, combo.Length - lastComboQuant);

        //remove from list
        int startIndex = platesAlreadyDone.Count - lastComboQuant - 1;
        if (lastComboQuant == 7)
        {
            startIndex++;
            lastComboQuant--;
        }

        for (int i = startIndex; i <= startIndex + lastComboQuant; i++)
        {
            GameObject plate = FindPlateById(platesAlreadyDone[i]);
            plate.GetComponent<Renderer>().material.color *= 2f;
        }

        platesAlreadyDone.RemoveRange(startIndex, lastComboQuant + 1);

    }

    private GameObject FindPlateById(int id)
    {
        // Find all GameObjects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Iterate through them and check their instance ID
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetInstanceID() == id)
                return obj;
        }

        // Return null if no GameObject with the given ID is found
        return null;
    }

    public bool IsAllUpper(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        foreach (char c in input)
        {
            if (!char.IsUpper(c) && char.IsLetter(c))
            {
                return false;
            }
        }

        return true;
    }

}
