using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attributesTxt;
    [SerializeField] private Text speedPriceTxt;
    [SerializeField] private Text jumpPriceTxt;
    private int speedPrice = 5;
    private int jumpPrice = 3;
    private void UpdateAttributesTxt()
    {
        attributesTxt.text = "Speed: " + Attributes.speed + "\nJump: " + Attributes.jump;

        UpdatePrices();
    }
    private void UpdatePrices()
    {
        speedPriceTxt.text = "Price: " + speedPrice + "Points";
        jumpPriceTxt.text = "Price: " + jumpPrice + "Points";
    }

    [SerializeField] private TextMeshProUGUI pointsTxt;
    public void UpdatePointsTxt() => pointsTxt.text = Attributes.points.ToString() + " Points";

    private void Start()
    {
        UpdateAttributesTxt();
        UpdatePointsTxt();
    }

    private bool EnoughtMoney(int price)
    {
        if (Attributes.points >= price)
        {
            Attributes.points -= price;
            UpdatePointsTxt();
            return true;
        }
        return false;
    }

    public void AddSpeed()
    {
        if (EnoughtMoney(speedPrice))
        {
            speedPrice++;
            Attributes.speed += 2;
            UpdateAttributesTxt();
        }
    }
    public void AddJump()
    {
        if (EnoughtMoney(jumpPrice))
        {
            jumpPrice *= 2;
            Attributes.jump += 1;
            UpdateAttributesTxt();
        }
    }
}
