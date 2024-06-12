using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerPointsTxt;

    [SerializeField] private Text speedLvlTxt;
    [SerializeField] private Text visionLvlTxt;
    [SerializeField] private Text pointsLvlTxt;

    [SerializeField] private Text speedTxt;
    [SerializeField] private Text visionTxt;
    [SerializeField] private Text pointsTxt;

    [SerializeField] private Text speedPriceTxt;
    [SerializeField] private Text visionPriceTxt;
    [SerializeField] private Text pointsPriceTxt;

    [SerializeField] private GameObject speedEmptyGOImg;
    [SerializeField] private GameObject visionEmptyGOImg;
    [SerializeField] private GameObject pointsEmptyGOImg;

    private void Start()
    {
        UpdateAttributesTxt();
        UpdatePointsTxt();

        speedEmptyGOImg.SetActive(GameManager.speedPerc >= 100);
        visionEmptyGOImg.SetActive(GameManager.visionPerc >= 100);
        pointsEmptyGOImg.SetActive(GameManager.pointsPerc >= 100);
    }

    private void UpdateAttributesTxt()
    {
        speedTxt.text = "+ " + GameManager.speedPerc + "%";
        visionTxt.text = "+ " + GameManager.visionPerc + "%";
        pointsTxt.text = "+ " + GameManager.pointsPerc + "%";

        UpdatePrices();
        UpdateLvls();
    }
    private void UpdatePrices()
    {
        speedPriceTxt.text = GameManager.speedPrice.ToString();
        visionPriceTxt.text = GameManager.visionPrice.ToString();
        pointsPriceTxt.text = GameManager.pointsPrice.ToString();
    }

    private void UpdateLvls()
    {
        speedLvlTxt.text = GameManager.speedLvl.ToString();
        visionLvlTxt.text = GameManager.visionLvl.ToString();
        pointsLvlTxt.text = GameManager.pointsLvl.ToString();
    }

    public void UpdatePointsTxt() => PlayerPointsTxt.text = GameManager.playerPoints + " Points";

    private bool EnoughtMoney(int price)
    {
        if (GameManager.playerPoints >= price)
        {
            GameManager.playerPoints -= price;
            UpdatePointsTxt();
            return true;
        }
        return false;
    }
    public void AddSpeed()
    {
        if (GameManager.speedPerc < 100 && EnoughtMoney(GameManager.speedPrice))
        {
            GameManager.speedPrice += 4;
            GameManager.speedPerc += 20;
            GameManager.speedLvl++;
            UpdateAttributesTxt();
        }
    }
    public void AddVision()
    {
        if (GameManager.visionPerc < 100 && EnoughtMoney(GameManager.visionPrice))
        {
            GameManager.visionPrice *= 2;
            GameManager.visionPerc += 20;
            GameManager.visionLvl++;
            UpdateAttributesTxt();
        }
    }
    public void AddPoints()
    {
        if (GameManager.pointsPerc < 100 && EnoughtMoney(GameManager.pointsPrice))
        {
            GameManager.pointsPrice *= 2;
            GameManager.pointsPerc += 10;
            GameManager.pointsLvl++;
            UpdateAttributesTxt();
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
