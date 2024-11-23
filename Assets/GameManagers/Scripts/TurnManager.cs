using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public TurnManager Instance;

    // Attach a function to this action to run when there's a new turn
    public static UnityAction NewTurn;

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
        }
    }
}
