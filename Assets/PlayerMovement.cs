using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private bool doLerpSmoothing = false;
    [SerializeField] private float lerpFactor = 5f;

    private Rigidbody2D rb;

    private Vector2 _inputDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateMove();
    }

    private void HandleInput()
    {
        _inputDir = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical"))
            .normalized;
    }

    private void UpdateMove()
    {
        if (doLerpSmoothing)
        {
            rb.velocity = Vector2.Lerp(
                rb.velocity, _inputDir * maxSpeed, 
                lerpFactor * Time.deltaTime);
        }
        else {
            rb.velocity = _inputDir * maxSpeed;
        }
    }
}
