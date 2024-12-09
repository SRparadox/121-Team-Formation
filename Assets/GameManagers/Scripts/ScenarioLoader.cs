using UnityEngine;

public class ScenarioLoader : MonoBehaviour
{
    // files that should be affected 


    // Scenario Info
    [System.Serializable]
    public class Scenario
    {
        public string ScenarioName;
        public WeatherEvent[] WeatherEvents;
        public VictoryCondition VictoryCondition;
    }

    [System.Serializable]
    public class WeatherEvent
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
        /*Debug.Log($"Loaded Scenario: {scenario.ScenarioName}");
        Debug.Log($"Victory Condition: {scenario.VictoryCondition.Harvest}");
        foreach (WeatherEvent e in scenario.WeatherEvents)
        {
            Debug.Log($"on Turn {e.Turn} trigger {e.EventType}");
        }*/

        // actual functionality
        UIManager.Instance.SetWinCondition(scenario.VictoryCondition.Harvest);
        TurnManager.SetLevelEvents(scenario.WeatherEvents);

    }
}
