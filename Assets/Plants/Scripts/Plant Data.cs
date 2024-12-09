using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Farming/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public float minWater = 0;
    public float minSun = 0;

    // would like to update to have max water/sun as well
    public int minTotalNeighbors = 0;
    public int minSpecificNeighbors = 0;

    //public List<string> preferredNeighborPlants;
    public List<string> invalidNeighborPlants;
    public List<string> requiredNeighborPlants;
    public List<Sprite> plantSprites;

    public bool CanGrow(GrowthContext context)
    {
        // Debugging setup
        /* Debug.Log($"CanGrow Debug - Plant: {plantName}, " +
                  $"Min Water: {minWater}, Min Sun: {minSun}, " +
                  $"Min Total Neighbors: {minTotalNeighbors}, Min Specific Neighbors: {minSpecificNeighbors}, " +
                  $"Invalid Neighbors: {string.Join(", ", invalidNeighborPlants)}, " +
                  $"Required Neighbors: {string.Join(", ", requiredNeighborPlants)}"); */

        // Check water and sun requirements
        float waterLevel = context.cell.GetWater();
        float sunLevel = context.cell.GetSun();

        bool waterValid = waterLevel >= minWater;
        bool sunValid = sunLevel >= minSun;

        // Neighbor-related checks
        int requiredNeighborsCount = 0;
        int invalidNeighborsCount = 0;
        int totalNeighborCount = 0;

        foreach (var neighbor in context.neighborCells)
        {
            if (neighbor != null && neighbor.GetPlant()?.plantData != null)
            {
                totalNeighborCount++;

                string neighborName = neighbor.GetPlant().plantData.plantName;

                // Count required neighbors
                if (requiredNeighborPlants.Contains(neighborName))
                {
                    requiredNeighborsCount++;
                }

                // Count invalid neighbors
                if (invalidNeighborPlants.Contains(neighborName))
                {
                    invalidNeighborsCount++;
                }
            }
        }

        bool enoughNeighbors = totalNeighborCount >= minTotalNeighbors;
        bool enoughRequired = requiredNeighborsCount >= minSpecificNeighbors;
        bool noInvalidNeighbors = invalidNeighborsCount == 0;

        // Final result
        bool canGrow = waterValid && sunValid && noInvalidNeighbors && enoughNeighbors && enoughRequired;

        // Debugging output
        /* Debug.Log($"CanGrow Debug - Plant: {plantName}, " +
                  $"Water Valid: {waterValid}, Sun Valid: {sunValid}, " +
                  $"No Invalid Neighbors: {noInvalidNeighbors} (Invalid Neighbors: {invalidNeighborsCount}), " +
                  $"Enough Total Neighbors: {enoughNeighbors} (Total Neighbors: {totalNeighborCount}), " +
                  $"Enough Required Neighbors: {enoughRequired} (Required Neighbors: {requiredNeighborsCount}), " +
                  $"Final Result: {canGrow}"); */

        return canGrow;
    }
}
