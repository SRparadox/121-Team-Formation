using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] PlantData plantData;
    [SerializeField] Map map;

    private int currentStage;
    private Cell cell;

    const int GROWTH_STAGES = 3; // Every plant has same num of growth stages

    public void Initialize(Cell cell, Map map, PlantData plantData)
    {
        this.cell = cell;
        this.currentStage = 0;
        this.map = map;
        this.plantData = plantData;
        UpdatePlantVisual();
    }

    private bool CheckValidNeighbors()
    {
        Vector3Int plantPosition = map.PosToCellCoord(transform.position);
        List<Vector3Int> neighbors = map.GetNeighbors(plantPosition);

        foreach (Vector3Int neighbor in neighbors) // For each adjacent plant
        {
            if (map.IsGroundCell(neighbor))
            {
                Cell neighborCell = map.GetCell(neighbor);
                Plant neighborPlant = neighborCell.GetPlant();

                if (neighborPlant != null)
                {
                    // Check if neighboring plant is part of the valid neighbor list
                    if (!plantData.preferredNeighborPlants.Contains(neighborPlant.plantData.plantName))
                    {
                        return false;
                    }
                }
            }
        }

        return true; // All neighbors are either empty or valid
    }

    public void CheckGrowthConditions()
    {
        float water = cell.GetWater();
        float sun = cell.GetSun();

        // Only grow plant if conditions are met
        // Might add a condition for growth time? or maybe add more complex conditions
        // i.e. water + sun must be within a certain threshold rather than just above
        if (water >= plantData.minWater && sun >= plantData.minSun && CheckValidNeighbors())
        {
            Grow();
        }

        //testing
        Debug.Log($"water threshold: {water >= plantData.minWater}");
        Debug.Log($"sun threshold: {sun >= plantData.minSun}");
        Debug.Log($"valid neighbors?: {CheckValidNeighbors()}");
    }

    private void Grow()
    {
        if (currentStage < GROWTH_STAGES)
        {
            currentStage++;
            UpdatePlantVisual();
        }
    }

    private void UpdatePlantVisual()
    {
        Debug.Log($"{GetName()} grew to stage {currentStage}."); // testing

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = plantData.plantSprites[currentStage];
    }

    public string GetName()
    {
        return plantData.plantName;
    }
}
