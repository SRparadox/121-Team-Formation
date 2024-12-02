using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Farming/PlantData")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public float minWater;
    public float minSun;
    //public float growthRate;
    public List<string> preferredNeighborPlants;

    public List<Sprite> plantSprites;

}
