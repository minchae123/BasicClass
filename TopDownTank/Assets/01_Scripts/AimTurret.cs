using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    public float rotationSpeed = 200;
    public float turretRotationSpeed = 150;

    public void Aim(Vector2 point)
    {
        var turrentDir = (Vector3)point - transform.position;
        var desireAngle = Mathf.Atan2(turrentDir.y, turrentDir.x) * Mathf.Rad2Deg;
        var roatationStep = turretRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desireAngle), roatationStep);
    }
}
