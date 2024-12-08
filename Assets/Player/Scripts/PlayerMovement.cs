using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] Map map;
    [SerializeField] GameObject plantPrefab;  
    [SerializeField] List<PlantData> plantDatas;
    private Rigidbody2D rb;

    private enum MovementType { Grid , Free };
    [Header("Movement Settings")]
    [SerializeField] private MovementType type;

    [Header("Parameters")]
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private bool doLerpSmoothing = false;
    [SerializeField] private float lerpFactor = 5f;

    private Vector2 _inputDir;
    private Vector3Int _targetCell;
    private Vector3 _targetPos;
    private Vector3Int _prevDirection;
    private float _speed;
    private int moveCount = 1;
    private int harvested = 0;

    const int MOVES_PER_TURN = 3;
    const float MIN_DIST = 0.001f;
    const float DEADZONE = 0.5f;

    private bool gameActive = true;
    
    public UnityAction<Vector3Int> PlayerMoved;

    // Start is called before the first frame update
    void Start()
    {
        LoadAllPlantData(); // generate list of all valid plant types at start

        rb = GetComponent<Rigidbody2D>();

        PlayerMoved += HandleTurns;
        TurnManager.EndGame += OnGameEnd;

        _targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetInputDirection();
        UpdateMove();

        // Planting mechanic (using "P" key for planting for now)
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlantAtCurrentCell();
        }

        // Reaping mechanic (using "O" key for reaping)
        if (Input.GetKeyDown(KeyCode.O))
        {
            HarvestAtCurrentCell();
        }
    }

    private void SetInputDirection()
    {
        _inputDir = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical"))
            .normalized;
    }

    private void UpdateMove()
    {
        if (gameActive == false)
        {
            return;
        }

        if (type == MovementType.Free)
        {
            rb.velocity = doLerpSmoothing ?
                Vector2.Lerp(rb.velocity, _inputDir * maxSpeed, lerpFactor * Time.deltaTime)
                : rb.velocity = _inputDir * maxSpeed;
            return;
        }

        if (IsMoving())
        {
            _speed = doLerpSmoothing ? 
                Mathf.Lerp(_speed, maxSpeed, lerpFactor * Time.deltaTime)
                : maxSpeed;

            transform.position = Vector3.MoveTowards(
                transform.position, _targetPos,
                _speed * Time.deltaTime);
        }
        else
        {
            _speed = doLerpSmoothing ?
                Mathf.Lerp(_speed, 0f, lerpFactor * Time.deltaTime)
                : 0f;

            HandleGridInputs();
        }
    }

    // New turn begins after a certain number of moves occur
    private void HandleTurns(Vector3Int direction)
    {
        moveCount++;

        if (moveCount >= MOVES_PER_TURN)
        {
            TurnManager.NextTurn();
            moveCount = 0;
        }
    }

    private void MoveTarget(Vector3Int direction)
    {
        if (direction == Vector3Int.zero || !map.IsGroundCell(_targetCell + direction))
        {
            return;
        }

        _targetCell += direction;
        _targetPos = GetTargetPosition();

        if (PlayerMoved != null)
        {
            _prevDirection = direction;
            PlayerMoved.Invoke(direction);
        }
        UIManager.Instance.UpdateCurrentCell(map.GetCell(_targetCell));
    }

    private void HandleGridInputs()
    {
        // Check for Inputs
        Vector3Int direction = Vector3Int.zero;
        if (Mathf.Abs(_inputDir.x) > DEADZONE)
        {
            direction += Vector3Int.right * (int)Mathf.Sign(_inputDir.x);
        }
        if (Mathf.Abs(_inputDir.y) > DEADZONE)
        {
            direction += Vector3Int.up * (int)Mathf.Sign(_inputDir.y);
        }

        // If both axis are pressed
        if (direction.magnitude > 1f)
        {
            // Prioritize the other axis
            direction *= Mathf.Abs(_prevDirection.x) > DEADZONE ?
                Vector3Int.up : Vector3Int.right;
        }

        MoveTarget(direction);
    }

    // Handle planting and Reaping
    private void PlantAtCurrentCell()
    {
        // Check if the current cell is a valid tilled cell and is not already occupied by a plant
        if (map.TilledCells.ContainsKey(_targetCell) && map.GetCell(_targetCell).GetPlant() == null)
        {
            // Instantiate the plant at the target position
            Plant newPlant = Instantiate(plantPrefab, map.CellCoordToPos(_targetCell), Quaternion.identity).GetComponent<Plant>();
            PlantData randomPlantData = plantDatas[Random.Range(0, plantDatas.Count)]; // right now a random plant type is selected. might change this logic
            newPlant.Initialize(map.GetCell(_targetCell), map, randomPlantData);

            map.AddPlant(newPlant);

            map.GetCell(_targetCell).SetPlant(newPlant);

            UIManager.Instance.UpdateCurrentCell(map.GetCell(_targetCell));
        }
        else
        {
            Debug.Log("Cannot plant here! Either the cell is occupied or invalid.");
        }
    }

    private void HarvestAtCurrentCell()
    {
        // Check if the current cell has a plant and if it is fully grown
        if (map.TilledCells.ContainsKey(_targetCell) && map.GetCell(_targetCell).GetPlant() != null)
        {
            Plant plant = map.GetCell(_targetCell).GetPlant();

            if (plant.IsFullyGrown())
            {
                // harvestScore += plant.GetScoreValue(); // if we want plants to have different score values
                harvested += 1;

                // update ui
                UIManager.Instance.UpdateScore(harvested);

                // remove the plant from the map and cell
                map.plants.Remove(plant);
                map.GetCell(_targetCell).SetPlant(null);
                Destroy(plant.gameObject); // could definitely change to handle w pooling

                Debug.Log($"Reaped {plant.GetName()}. Score: {harvested}");
            }
            else
            {
                Debug.Log("Plant is not fully grown yet!");
            }
        }
        else
        {
            Debug.Log("No plant to reap here!");
        }
    }

    public void OnGameEnd()
    {
        UIManager.Instance.EnableGameOverUI(harvested);

        gameActive = false;
    }

    private Vector3 GetTargetPosition()
    {
        return map.CellCoordToPos(_targetCell);
    }

    private bool IsMoving()
    {
        return (transform.position - _targetPos).magnitude > MIN_DIST;
    }

    void LoadAllPlantData()
    {
        // Find all PlantData assets in the project folder
        string[] guids = AssetDatabase.FindAssets("t:PlantData", new[] { "Assets/Plants/Scripts" });

        plantDatas.Clear();

        // Loop through each GUID and load the corresponding asset
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            PlantData plantData = AssetDatabase.LoadAssetAtPath<PlantData>(path);

            if (plantData != null)
            {
                plantDatas.Add(plantData); // Add the PlantData asset to the list
            }
        }
    }
}
