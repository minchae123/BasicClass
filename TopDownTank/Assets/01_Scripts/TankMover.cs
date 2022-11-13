using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveVector;

    public float maxSpeed = 70;
    public float rotationSpeed = 200;
    public float acceleration = 70;
    public float deacceleration = 50;

    private float currentSpeed = 0;
    private float currentForewardDirection = 1;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 move)
    {
        moveVector = move;
        CalculateSpeed(move);
        if(move.y > 0)
        {
            currentForewardDirection = 1;
        }
        else if(move.y < 0)
        {
            currentForewardDirection = -1;
        }
    }

    private void CalculateSpeed(Vector2 move)
    {
        if(Mathf.Abs(move.y) > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector2)transform.up * currentSpeed * currentForewardDirection * Time.fixedDeltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -moveVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}
