using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    private Camera cam;

    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMovebody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurrent = new UnityEvent<Vector2>();

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
        GetShootingInput();
    }

    public void GetShootingInput()
    {
        //OnShoot.AddListener(GetShootingInput); // 유니티 이벤트에 넣어줌
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
            if (OnShoot != null) OnShoot?.Invoke();
        }
    }

    public void GetTurretMovement()
    {
        OnMoveTurrent?.Invoke(GetMousePosition());
    }

    public Vector2 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition; // screen
        mousePos.z = cam.nearClipPlane;
        Vector2 mouseWorldPos = cam.ScreenToWorldPoint(mousePos); // world
        return mouseWorldPos;
    }

    public void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMovebody?.Invoke(movementVector.normalized);

    }
}
