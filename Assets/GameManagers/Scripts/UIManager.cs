using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("General UI Elements")]
    [SerializeField] public TextMeshProUGUI harvestedText;
    [SerializeField] public TextMeshProUGUI dayDisplay;

    [Header("Current Cell Elements")]
    [SerializeField] public Canvas CurrentCell;
    [SerializeField] public TextMeshProUGUI currentCellPlant;
    [SerializeField] public TextMeshProUGUI currentCellSun;
    [SerializeField] public TextMeshProUGUI currentCellWater;

    [Header ("GameOver Elements")]
    [SerializeField] public Canvas GameOverPanel;
    [SerializeField] public TextMeshProUGUI scoreDisplay;
    [SerializeField] public TextMeshProUGUI resultsDisplay;

    const int WIN_CONDITION = 10;


    private int harvested = 0;
    private int currentDay = 0;
    private string currentPlant;
    private float currentSun;
    private float currentWater;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Call this to update the score UI
    public void UpdateScore(int newScore)
    {
        harvested = newScore;
        harvestedText.text = $"Harvested: {harvested}";
    }

    public void UpdateDay(int day)
    {
        currentDay = day;
        dayDisplay.text = $"Day: {currentDay}";

    }

    public void UpdateCurrentCell(Cell cell)
    {
        UpdateSun(cell.GetSun());
        UpdateWater(cell.GetWater());
        UpdateCurrentPlant(cell.GetPlant());
    }

    public void UpdateCurrentPlant(Plant plant)
    {
        if (plant == null)
        {
            currentPlant = "none";
        }
        else
        {
            currentPlant = plant.GetName();
        }
        currentCellPlant.text = $"Plant: {currentPlant}";
        
    }
    public void UpdateSun(float sunlvl)
    {
        currentSun = Mathf.Round(sunlvl * 100f) / 100f;
        currentCellSun.text = $"Sun: LVL {currentSun:f2}";

    }

    public void UpdateWater(float waterlvl)
    {
        currentWater = Mathf.Round(waterlvl * 100f) / 100f;
        currentCellWater.text = $"Water: LVL {currentWater:F2}";

    }

    public void EnableGameOverUI(int score)
    {
        CurrentCell.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(true);
        scoreDisplay.text = $"You harvested {score} crops!";

        if (score > WIN_CONDITION)
        {
            scoreDisplay.text = "A Great Harvest!! You Won !!!";
            
        }
        else
        {
            resultsDisplay.text = "A Poor Harvest. You lost !!!";
        }

    }

}
