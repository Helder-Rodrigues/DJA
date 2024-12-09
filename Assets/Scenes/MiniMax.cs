using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MinimaxGame : MonoBehaviour
{
    public TextMeshProUGUI playerChoiceText, resultText;
    public Button[] pairButtons; // Exactly 12 buttons assigned in the Inspector
    private List<int> currentNumbers = new List<int>();
    private List<int> selectedNumbers = new List<int>(); // List to store numbers selected in Round 1
    private int currentPairIndex = 0; // Tracks which pair the player is choosing from
    private bool isPlayerTurn = false; // Variable to track whose turn it is (true = player, false = CPU)
    private bool isSecondRound = false; // Track if we are in Round 2
    private bool isThirdRound = false; // Track if we are in Round 3

    private List<int> finalNumbers = new List<int>(); // Stores the 3 numbers for Round 3


    void Start()
    {
        StartGame();
    }

    // Initialize the game
    void StartGame()
    {
        GenerateInitialNumbers(); // Generate the 6 pairs of numbers
        SetButtonListeners(); // Set the button listeners
        UpdateUI(); // Update the UI to display the first pair
    }

    // Generate the 6 pairs of numbers
    void GenerateInitialNumbers()
    {
        currentNumbers.Clear();
        for (int i = 0; i < 12; i++)
        {
            currentNumbers.Add(Random.Range(15, 26)); // Random numbers between 15 and 25
        }
    }

    // Assign listeners to the buttons
    void SetButtonListeners()
    {
        for (int i = 0; i < pairButtons.Length; i++)
        {
            int index = i; // Capture the index for the listener
            pairButtons[i].onClick.RemoveAllListeners();
            pairButtons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    // Update the UI to show the current pair and disable others
    void UpdateUI()
    {
        DisableAllButtons(); // Disable all buttons initially

        if (isSecondRound)
        {
            // Handle second round: form new pairs from the selected numbers
            HandleSecondRound();
            return;
        }

        if (isThirdRound)
        {
            // Handle second round: form new pairs from the selected numbers
            HandleThirdRound();
            return;
        }

        if (currentPairIndex < 6)
        {
            int left = currentNumbers[currentPairIndex * 2];
            int right = currentNumbers[currentPairIndex * 2 + 1];

            // Activate the current pair
            pairButtons[currentPairIndex * 2].gameObject.SetActive(true);
            pairButtons[currentPairIndex * 2 + 1].gameObject.SetActive(true);

            pairButtons[currentPairIndex * 2].GetComponentInChildren<TextMeshProUGUI>().text = left.ToString();
            pairButtons[currentPairIndex * 2 + 1].GetComponentInChildren<TextMeshProUGUI>().text = right.ToString();

            // Make only the current pair interactable
            pairButtons[currentPairIndex * 2].interactable = true;
            pairButtons[currentPairIndex * 2 + 1].interactable = true;

            if (isPlayerTurn)
            {
                playerChoiceText.text = "Your turn! Choose between " + left + " and " + right;
            }
            else
            {
                // This is where we will simulate the CPU's choice
                playerChoiceText.text = "CPU's turn! It will choose the lower number.";
                SimulateCPUChoice(left, right);
            }

            playerChoiceText.text = "Choose between " + left + " and " + right;
        }
    }


     // Simulate CPU choice (for now, just log which number it would choose)
   void SimulateCPUChoice(int left, int right)
{
    int cpuChoice = Mathf.Min(left, right); // CPU chooses the smaller number
    Debug.Log("CPU chooses: " + cpuChoice);
    resultText.text = "CPU chooses: " + cpuChoice;

    // Add CPU's choice to the selectedNumbers list
    selectedNumbers.Add(cpuChoice);

    // After the CPU's choice, switch to the player's turn
    isPlayerTurn = true; // Now it's the player's turn again
    currentPairIndex++;
    UpdateUI(); // Update UI for the next pair
}

    // Handle the player's choice
    void OnButtonClicked(int buttonIndex)
{
    if (buttonIndex < 0 || buttonIndex >= pairButtons.Length)
    {
        Debug.LogError("Button index is out of bounds: " + buttonIndex);
        return;
    }

    Debug.Log("Button clicked: " + buttonIndex);

    // Get the chosen number based on currentNumbers or finalNumbers
    int chosenNumber = int.Parse(pairButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text);
    resultText.text = "You chose: " + chosenNumber;

    if (isSecondRound)
    {
        finalNumbers.Add(chosenNumber);
    }
    else if (isThirdRound)
    {
        // Disable all buttons after the player's final choice in Round 3
        DisableAllButtons();
        resultText.text = "The remaining number is: " + chosenNumber;
    }
    else
    {
        selectedNumbers.Add(chosenNumber); // Add to Round 2 selection
    }

    // Disable the current pair after the player's choice (but keep it visible)
    pairButtons[buttonIndex].interactable = false;

    // Disable the other number in the pair
    int otherIndex = buttonIndex % 2 == 0 ? buttonIndex + 1 : buttonIndex - 1;
    pairButtons[otherIndex].interactable = false;

    // Move to the next pair
    currentPairIndex++;

    if (!isSecondRound && !isThirdRound && currentPairIndex >= 6)
    {
        // End of Round 1
        resultText.text = "Round 1 complete! Moving to Round 2.";
        currentNumbers = new List<int>(selectedNumbers); // Update for Round 2
        selectedNumbers.Clear();
        isSecondRound = true;
        currentPairIndex = 0;
        Debug.Log("currentNumbers: " + string.Join(", ", currentNumbers));
        Debug.Log("selectedNumbers: " + string.Join(", ", selectedNumbers));
        UpdateUI();
    }
    else if (isSecondRound && currentPairIndex >= 3)
    {
        // End of Round 2
        Debug.Log("Round 2 complete! Moving to Round 3.");
        resultText.text = "Round 2 complete! Moving to Round 3.";
        currentNumbers = new List<int>(finalNumbers); // Update for Round 3
        isSecondRound = false;
        isThirdRound = true;
        currentPairIndex = 0;
        UpdateUI();
    }
    else
    {
        UpdateUI(); // Update the UI for the next pair
    }

    // Switch turn after the player has made their choice
    isPlayerTurn = false;
    UpdateUI(); // Update the UI to show it's the CPU's turn
}

void HandleSecondRound()
{
    Debug.Log("currentNumbers: " + string.Join(", ", currentNumbers));
    Debug.Log("selectedNumbers: " + string.Join(", ", selectedNumbers));

    if (currentPairIndex < 3)
    {
        for (int i = 0; i < 3; i++)
        {
            int leftTwo = currentNumbers[i * 2];
            int rightTwo = currentNumbers[i * 2 + 1];

            pairButtons[i * 2].GetComponentInChildren<TextMeshProUGUI>().text = leftTwo.ToString();
            pairButtons[i * 2 + 1].GetComponentInChildren<TextMeshProUGUI>().text = rightTwo.ToString();
        }

        int left = currentNumbers[currentPairIndex * 2];
        int right = currentNumbers[currentPairIndex * 2 + 1];

        pairButtons[currentPairIndex * 2].gameObject.SetActive(true);
        pairButtons[currentPairIndex * 2 + 1].gameObject.SetActive(true);
        pairButtons[currentPairIndex * 2].interactable = true;
        pairButtons[currentPairIndex * 2 + 1].interactable = true;

    }

    for (int i = 6; i < 12; i++)
    {
        pairButtons[i].gameObject.SetActive(false); // Hide unused buttons
    }
}

void HandleThirdRound()
{
    DisableAllButtons(); // Disable all buttons initially

    Debug.Log("Final Numbers: " + string.Join(", ", finalNumbers));

    // Ensure that there are exactly 3 numbers in finalNumbers
    if (finalNumbers.Count != 3)
    {
        Debug.LogError("Final numbers are not correctly populated for Round 3.");
        return;
    }

    // Display the remaining 3 numbers on the first 3 buttons
    for (int i = 0; i < 3; i++)
    {
        pairButtons[i].gameObject.SetActive(true); // Make sure the buttons are active
        pairButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = finalNumbers[i].ToString(); // Set the number text
        pairButtons[i].interactable = true; // Make the button interactable
    }

    // Disable all other buttons (not part of Round 3)
    for (int i = 3; i < pairButtons.Length; i++)
    {
        pairButtons[i].gameObject.SetActive(false);
    }

    playerChoiceText.text = "Choose your final number";
}



    // Disable all buttons
    void DisableAllButtons()
    {
        foreach (var button in pairButtons)
        {
            button.interactable = false; // Disable interaction
            button.gameObject.SetActive(true); // Keep buttons visible
        }
    }

}