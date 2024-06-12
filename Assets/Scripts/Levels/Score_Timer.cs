using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score_Timer : MonoBehaviour
{
    [SerializeField] private Texture emptyStarTex;
    [SerializeField] private Texture filledStarTex;

    [SerializeField] private TextMeshProUGUI timerTxt;

    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private TextMeshProUGUI highScoreTxt;

    [SerializeField] private GameObject starsPanel;

    private float timer;
    private bool isTimerRunning = true;
    static public bool stopTimer;

    private readonly int[] fastTimeSec = { 30, 90, 60 };
    private readonly int[] mediumTimeSec = { 60, 150, 120 };
    private readonly int[] slowTimeSec = { 120, 360, 180 };

    private void Start()
    {
        stopTimer = false;
        timer = 0f;
        UpdateTimerText();
    }

    private void Update()
    {
        if (stopTimer)
        {
            if (isTimerRunning)
                StartCoroutine(DetermineResult());
            isTimerRunning = false;
        }

        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private IEnumerator DetermineResult()
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(2);

        starsPanel.SetActive(true);

        int stars = 0;
        if (timer <= fastTimeSec[GameManager.CurrLvl])
            stars = 3;
        else if (timer <= mediumTimeSec[GameManager.CurrLvl])
            stars = 2;
        else if (timer <= slowTimeSec[GameManager.CurrLvl])
            stars = 1;

        GameManager.playerPoints += (int)(stars * 5 * (1 + GameManager.pointsPerc / 100));
        AddStars(stars);

        highScorePanel.SetActive(true);
        bool newHighScore = false;
        if (GameManager.CurrLvl == 0 && (timer < Lvl1Manager.highScore || Lvl1Manager.highScore == 0))
        {
            newHighScore = true;
            Lvl1Manager.highScore = timer;
        }
        else if (GameManager.CurrLvl == 1 && (timer < Lvl2Manager.highScore || Lvl2Manager.highScore == 0))
        {
            newHighScore = true;
            Lvl2Manager.highScore = timer;
        }
        else if (GameManager.CurrLvl == 2 && (timer < Lvl3Manager.highScore || Lvl3Manager.highScore == 0))
        {
            newHighScore = true;
            Lvl3Manager.highScore = timer;
        }

        if (newHighScore)
            highScoreTxt.text = "New Highscore:\n" + timer.ToString("000.00");
        else
            highScoreTxt.text = "Highscore:\n" + (GameManager.CurrLvl == 0 ? Lvl1Manager.highScore : GameManager.CurrLvl == 1 ? Lvl2Manager.highScore : Lvl3Manager.highScore).ToString("000.00");

        yield return new WaitForSecondsRealtime(3);

        int lvlUnlock = GameManager.CurrLvl + 2;
        if (GameManager.lvlsUnlocked < lvlUnlock)
            GameManager.lvlsUnlocked = lvlUnlock;

        Time.timeScale = 1;
        SceneManager.LoadScene(2); //Lvls Scene
    }

    private void AddStars(int amount)
    {
        for (int i = 0; i < amount; i++)
            GameObject.Find("star" + (i + 1)).GetComponent<RawImage>().texture = filledStarTex;

        for (int i = amount; i < 3; i++)
            GameObject.Find("star" + (i + 1)).GetComponent<RawImage>().texture = emptyStarTex;
    }

    private void UpdateTimerText()
    {
        timerTxt.text = "Timer: " + timer.ToString("000.00");
    }
}
