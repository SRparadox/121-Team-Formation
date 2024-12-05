using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public TurnManager Instance;
    private static int currentTurn = 0;

    // Attach a function to this action to run when there's a new turn
    public static UnityAction NewTurn;
    public static UnityAction EndGame;

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
            Debug.Log($"Turn {currentTurn} has started.");
            UIManager.Instance.UpdateDay(currentTurn);
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

}
