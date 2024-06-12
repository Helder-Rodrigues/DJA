using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lvl2Txt;
    [SerializeField] private TextMeshProUGUI lvl3Txt;

    void Start()
    {
        if (GameManager.lvlsUnlocked > 1)
        {
            lvl2Txt.text = "Play";
            if (GameManager.lvlsUnlocked > 2)
                lvl3Txt.text = "Play";
        }
    }

    public void LoadLvl1()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadLvl2()
    {
        if (lvl2Txt.text == "Play")
            SceneManager.LoadScene(4);
    }
    public void LoadLvl3()
    {
        if (lvl3Txt.text == "Play")
            SceneManager.LoadScene(5);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
