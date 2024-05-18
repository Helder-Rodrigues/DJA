using UnityEngine;
using TMPro;

public class Score_Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private Shop shop;

    private float timer;
    private bool isTimerRunning;

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
        if (timer <= 3f)
            Attributes.points += 1;
        else if (timer > 3f && timer < 7f)
            Attributes.points += 3;
        else
            Attributes.points += 0;

        shop.UpdatePointsTxt();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        float seconds = timer % 60;
        timerText.text = "Timer: " + minutes.ToString("00") + ":" + seconds.ToString("00.0");
    }
}
