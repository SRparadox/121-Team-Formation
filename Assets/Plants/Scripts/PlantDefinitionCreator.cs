using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public static class PlantDefinitionCreator
{
    [MenuItem("Plants/Generate Plant Definitions")]
    public static void GeneratePlantDefinitions()
    {
        // Load all sprites from the sprite sheet folder using Resources.LoadAll
        // Load all sprites from the sprite sheet folder using Resources.LoadAll
        var allSprites = Resources.LoadAll<Sprite>("Plants/Farming Plants");

        // FOR DEVS: list of plant types with available art !
        // - cabbage, carrot, cauliflower, cucumber, eggplant, flower,
        // lettuce, parsnip, pumpkin, starfruit, tomato, turnip, wheat

        // Sprite Storage 
        var cabbageSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "cabbage_0"),
            GetSpriteByName(allSprites, "cabbage_1"),
            GetSpriteByName(allSprites, "cabbage_2"),
            GetSpriteByName(allSprites, "cabbage_3"),
        };

        var carrotSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "carrot_0"),
            GetSpriteByName(allSprites, "carrot_1"),
            GetSpriteByName(allSprites, "carrot_2"),
            GetSpriteByName(allSprites, "carrot_3"),
        };

        var cauliflowerSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "cauliflower_0"),
            GetSpriteByName(allSprites, "cauliflower_1"),
            GetSpriteByName(allSprites, "cauliflower_2"),
            GetSpriteByName(allSprites, "cauliflower_3"),
        };

        var cucumberSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "cucumber_0"),
            GetSpriteByName(allSprites, "cucumber_1"),
            GetSpriteByName(allSprites, "cucumber_2"),
            GetSpriteByName(allSprites, "cucumber_3"),
        };

        var eggplantSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "eggplant_0"),
            GetSpriteByName(allSprites, "eggplant_1"),
            GetSpriteByName(allSprites, "eggplant_2"),
            GetSpriteByName(allSprites, "eggplant_3"),
        };

        var flowerSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "flower_0"),
            GetSpriteByName(allSprites, "flower_1"),
            GetSpriteByName(allSprites, "flower_2"),
            GetSpriteByName(allSprites, "flower_3"),
        };

        var lettuceSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "lettuce_0"),
            GetSpriteByName(allSprites, "lettuce_1"),
            GetSpriteByName(allSprites, "lettuce_2"),
            GetSpriteByName(allSprites, "lettuce_3"),
        };

        var parsnipSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "parsnip_0"),
            GetSpriteByName(allSprites, "parsnip_1"),
            GetSpriteByName(allSprites, "parsnip_2"),
            GetSpriteByName(allSprites, "parsnip_3"),
        };

        var pumpkinSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "pumpkin_0"),
            GetSpriteByName(allSprites, "pumpkin_1"),
            GetSpriteByName(allSprites, "pumpkin_2"),
            GetSpriteByName(allSprites, "pumpkin_3"),
        };

        var starfruitSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "starfruit_0"),
            GetSpriteByName(allSprites, "starfruit_1"),
            GetSpriteByName(allSprites, "starfruit_2"),
            GetSpriteByName(allSprites, "starfruit_3"),
        };

        var tomatoSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "tomato_0"),
            GetSpriteByName(allSprites, "tomato_1"),
            GetSpriteByName(allSprites, "tomato_2"),
            GetSpriteByName(allSprites, "tomato_3"),
        };

        var turnipSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "turnip_0"),
            GetSpriteByName(allSprites, "turnip_1"),
            GetSpriteByName(allSprites, "turnip_2"),
            GetSpriteByName(allSprites, "turnip_3"),
        };

        var wheatSprites = new List<Sprite>
        {
            GetSpriteByName(allSprites, "wheat_0"),
            GetSpriteByName(allSprites, "wheat_1"),
            GetSpriteByName(allSprites, "wheat_2"),
            GetSpriteByName(allSprites, "wheat_3"),
        };

        // Plant Definitions Go Here !! vv
        // would like to update to have more complex conditions if possible

        // Parsnip Definition
        new PlantDSL()
            .Name("Parsnip")
            .MinimumWater(1.1f)
            .PreferredNeighbors("Lettuce, Cucumber, Wheat, Parsnip")
            .MinimumTotalNeighbors(1)
            .Sprites(parsnipSprites)
            .Build("Assets/Plants/Scripts/Parsnip.asset");
        // parsnip needs one neighbor of any type to grow and a little water

        // Lettuce Definition
        new PlantDSL()
            .Name("Lettuce")
            .MinimumWater(5f)
            .PreferredNeighbors("Lettuce, Cucumber, Parsnip")
            .MinimumTotalNeighbors(2)
            .RequiredNeighborTypes("Lettuce")
            .Sprites(lettuceSprites)
            .Build("Assets/Plants/Scripts/Lettuce.asset");

        // lettuce needs at least 1 lettuce neighbor, a total of 2 neighbors, and plenty of water

        //Wheat Definition
        new PlantDSL()
            .Name("Wheat")
            .MinimumWater(2f)
            .MinimumSun(1.5f)
            .PreferredNeighbors("Wheat, Parsnip")
            .RequiredNeighborTypes("Wheat")
            .MinimumSpecificNeighbors(3)
            .Sprites(wheatSprites)
            .Build("Assets/Plants/Scripts/Wheat.asset");
        // wheat needs at least 3 wheat neighbors and moderate sun/water

        //Cucumber Definition
        new PlantDSL()
            .Name("Cucumber")
            .MinimumWater(10f)
            .MinimumSun(1.7f)
            .PreferredNeighbors("Wheat, Parsnip, Cucumber, Lettuce")
            .Sprites(cucumberSprites)
            .Build("Assets/Plants/Scripts/Wheat.asset");
        // cucumber only needs lots of sun and water

        Debug.Log("Plant definitions created!");


    }



    // get sprites by name from the loaded sprite array
    private static Sprite GetSpriteByName(Sprite[] allSprites, string name)
    {
        foreach (var sprite in allSprites)
        {
            if (sprite.name == name)
                return sprite;
        }
        return null;
    }
}
