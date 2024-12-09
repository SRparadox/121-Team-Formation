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

    public List<string> preferredNeighborPlants;
    public List<string> requiredNeighborPlants;
    public List<Sprite> plantSprites;

    // checks are functional but sometimes difficult to fulfill. might need to tweak game logics
    public bool CanGrow(GrowthContext context)
    {
        // Testing
        /*Debug.Log($"CanGrow Debug - Plant: {plantName}, " +
                  $"Min Water: {minWater}, Min Sun: {minSun}, " +
                  $"Min Total Neighbors: {minTotalNeighbors}, Min Specific Neighbors: {minSpecificNeighbors}, " +
                  $"Preferred Neighbors: {string.Join(", ", preferredNeighborPlants)}, " +
                  $"Required Neighbors: {string.Join(", ", requiredNeighborPlants)}");*/

        // Check water and sun reqs
        bool waterTrue = context.cell.GetWater() >= minWater;
        bool sunTrue = context.cell.GetSun() >= minSun;

        // Neighbor-related checks
        int requiredNeighborsCount = 0;
        bool validNeighbors = true;

        int neighborCount = 0;

        foreach (var neighbor in context.neighborCells)
        {
            if (neighbor != null && neighbor.GetPlant()?.plantData != null)
            {
                neighborCount++;

                var neighborName = neighbor.GetPlant().plantData.plantName;
                Debug.Log($"neighbor: {neighborName}");

                if (requiredNeighborPlants.Contains(neighborName))
                {
                    requiredNeighborsCount++;
                }

                if (
                    !preferredNeighborPlants.Contains(neighborName)
                    && !requiredNeighborPlants.Contains(neighborName)
                )
                {
                    validNeighbors = false;
                }
            }
        }

        bool enoughNeighbors = neighborCount >= minTotalNeighbors;

        bool enoughRequired = requiredNeighborsCount >= minSpecificNeighbors;

        // Final result
        bool canGrow = waterTrue && sunTrue && validNeighbors && enoughNeighbors && enoughRequired;

        // Testing
        /*
        Debug.Log($"CanGrow Debug - Plant: {plantName}, " +
                  $"Water: {waterTrue}, Sun: {sunTrue}, " +
                  $"Valid Neighbors: {validNeighbors}, Enough Neighbors: {enoughNeighbors}, " +
                  $"Enough Required: {enoughRequired}, Final Result: {canGrow}"); */
        return canGrow;
    }
}
