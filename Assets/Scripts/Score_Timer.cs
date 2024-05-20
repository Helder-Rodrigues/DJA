using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score_Timer : MonoBehaviour
{
    [SerializeField] private Texture emptyStarTex;
    [SerializeField] private Texture filledStarTex;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private Shop shop;

    private float timer;
    private bool isTimerRunning;

    private readonly int[] fastTimeMin = { 3, 6, 9 };
    private readonly int[] mediumTimeMin = { 6, 10, 15 };
    private readonly int[] slowTimeMin = { 10, 15, 25 };

    private void Start()
    {
        timer = 0f;
        isTimerRunning = false;
        UpdateTimerText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTimerRunning)
                DetermineResult();
            else
                timer = 0f;

            isTimerRunning = !isTimerRunning;
        }

        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void DetermineResult()
    {
        int stars = 0;
        if (timer <= fastTimeMin[Attributes.CurrLvl])
            stars = 3;
        else if (timer <= mediumTimeMin[Attributes.CurrLvl])
            stars = 2;
        else if (timer <= slowTimeMin[Attributes.CurrLvl])
            stars = 1;

        Attributes.points += stars;
        AddStars(stars);

        shop.UpdatePointsTxt();
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
        int minutes = Mathf.FloorToInt(timer / 60);
        float seconds = timer % 60;
        timerText.text = "Timer: " + minutes.ToString("00") + ":" + seconds.ToString("00.0");
    }
}
