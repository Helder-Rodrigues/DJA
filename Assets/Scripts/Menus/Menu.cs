using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    private void Awake()
    {
        if (!GameManager.GameLoaded)
        {
            GameManager.LoadGame();
            GameManager.GameLoaded = true;
        }
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        GameManager.SaveGame();
        Application.Quit();
    }
}
