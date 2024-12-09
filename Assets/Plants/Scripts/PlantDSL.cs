using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlantDSL
{
    private PlantData plantData;

    public PlantDSL()
    {
        plantData = ScriptableObject.CreateInstance<PlantData>();

        // Initialize fields to defaults
        plantData.minSun = 0;
        plantData.minWater = 0;
        plantData.minTotalNeighbors = 0;
        plantData.minSpecificNeighbors = 0;

        // Initialize lists to empty
        plantData.invalidNeighborPlants = new List<string>();
        plantData.requiredNeighborPlants = new List<string>();
        plantData.plantSprites = new List<Sprite>();
    }

    public PlantDSL Name(string name)
    {
        plantData.plantName = name;
        return this;
    }

    public PlantDSL MinimumSun(float? sun)
    {
        plantData.minSun = sun ?? 0;
        return this;
    }

    public PlantDSL MinimumWater(float? water)
    {
        plantData.minWater = water ?? 0;
        return this;
    }

    public PlantDSL PreferredNeighbors(params string[] neighbors)
    {
        if (neighbors != null)
        {
            //plantData.preferredNeighborPlants = new List<string>(neighbors);
        }
        return this;
    }

    public PlantDSL InvalidNeighbors(params string[] neighbors)
    {
        if (neighbors != null)
        {
            plantData.invalidNeighborPlants = new List<string>(neighbors);
        }
        return this;

    }

        public PlantDSL RequiredNeighbors(params string[] neighbors)
    {
        if (neighbors != null)
        {
            plantData.requiredNeighborPlants = new List<string>(neighbors);
        }
        return this;
    }

    public PlantDSL MinimumTotalNeighbors(int? total)
    {
        plantData.minTotalNeighbors = total ?? 0;
        return this;
    }

    public PlantDSL MinimumSpecificNeighbors(int? total)
    {
        plantData.minSpecificNeighbors = total ?? 0;
        return this;
    }

    public PlantDSL Sprites(List<Sprite> sprites)
    {
        if (sprites != null)
        {
            plantData.plantSprites = sprites;
        }
        return this;
    }

    public PlantData Build(string assetPath = null)
    {
#if UNITY_EDITOR
        if (!string.IsNullOrEmpty(assetPath))
        {
            AssetDatabase.CreateAsset(plantData, assetPath);
            AssetDatabase.SaveAssets();
        }
#endif
        return plantData;
    }
}
