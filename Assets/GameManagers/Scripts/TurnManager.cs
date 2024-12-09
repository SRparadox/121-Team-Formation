using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ScenarioLoader;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    private static int currentTurn = 0;

    // list of events
    private static ScenarioLoader.WeatherEvent[] plannedEvents; 

    // Attach a function to this action to run when there's a new turn
    public static UnityAction NewTurn;
    public static UnityAction EndGame;

    [SerializeField] Map map;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Call to trigger a new turn
    public static void NextTurn()
    {
        if (NewTurn != null)
        {
            NewTurn.Invoke();
            currentTurn++;
            UIManager.Instance.UpdateDay(currentTurn);
            TriggerTurnEvents();
            Debug.Log($"Turn {currentTurn} has started.");
        }

        if (currentTurn == 31)
        {
            Debug.Log("Turn 31 reached. Ending game...");
            if (EndGame != null)
            {
                EndGame.Invoke();
            }
        }
    }

    public static void SetLevelEvents(ScenarioLoader.WeatherEvent[] events)
    {
        plannedEvents = events;
    }

    private static void TriggerTurnEvents()
    {
        foreach (var plannedEvent in plannedEvents)
        {
            if (plannedEvent.Turn == currentTurn)
            {
                TriggerEvent(plannedEvent);
            }
        }
    }

    private static void TriggerEvent(WeatherEvent plannedEvent)
    {
        Debug.Log($"Triggering event '{plannedEvent.EventType}' on Turn {currentTurn}");

        // Call the map function based on the event type
        if (plannedEvent.EventType == "Rain")
        {
            Instance.map.OnRainTriggered();
        }
        else if (plannedEvent.EventType == "Drought")
        {
            Instance.map.OnDroughtTriggered();
        }
        else
        {
            Debug.Log($"Event '{plannedEvent.EventType}' triggered with no specific handler.");
        }
    }

}
