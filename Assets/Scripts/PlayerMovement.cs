using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed;
    public float movementSmoothing;

    private Vector2 moveInput;
    private float currentVelocityX;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        var targetVelocity = moveInput * movementSpeed;

        // Smoothly interpolate velocity using Mathf.SmoothDamp
        rb.velocity = new Vector2(targetVelocity.x, targetVelocity.y);
    }

    public void OnMove(InputValue value) {
        Debug.Log("OnMove");
        moveInput = new Vector2(value.Get<Vector2>().x, value.Get<Vector2>().y).normalized;
    }
}