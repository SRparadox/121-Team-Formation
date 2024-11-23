using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] Map map;
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

    const float MIN_DIST = 0.001f;
    const float DEADZONE = 0.5f;

    public UnityAction<Vector3Int> PlayerMoved;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PlayerMoved += _ => TurnManager.NextTurn();

        _targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetInputDirection();
        UpdateMove();
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

        Debug.Log(map.GetCell(_targetCell));
    }

    private Vector3 GetTargetPosition()
    {
        return map.CellCoordToPos(_targetCell);
    }

    private bool IsMoving()
    {
        return (transform.position - _targetPos).magnitude > MIN_DIST;
    }
}
