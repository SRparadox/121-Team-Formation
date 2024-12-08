using UnityEngine;

public class ScenarioLoader : MonoBehaviour
{
    [System.Serializable]
    public class Scenario
    {
        public string ScenarioName;
        public Event[] WeatherEvents;
        public VictoryCondition VictoryCondition;
    }

    [System.Serializable]
    public class Event
    {
        public int Turn;
        public string EventType;
        // public Parameters Parameters;
    }

    [System.Serializable]
    public class VictoryCondition
    {
        public int Harvest;
    }

    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Scenarios/Scenario");
        Scenario scenario = JsonUtility.FromJson<Scenario>(jsonFile.text);

        // testing !!!
        Debug.Log($"Loaded Scenario: {scenario.ScenarioName}");
        Debug.Log($"Victory Condition: {scenario.VictoryCondition.Harvest}");
        foreach (Event e in scenario.WeatherEvents)
        {
            Debug.Log($"on Turn {e.Turn} trigger {e.EventType}");
        }
        // still have to implement this functionality in the actual game
    }
}
