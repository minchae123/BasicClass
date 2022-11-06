using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveVector;

    public float maxSpeed = 70;
    public float rotationSpeed = 200;


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 move)
    {
        moveVector = move;
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector2)transform.up * moveVector.y * maxSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -moveVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}
