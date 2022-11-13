using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public int damage = 5;
    public float maxDistance = 10;

    private Vector2 startPos;
    private float conquaredDistance = 0; // 거리 계산해서 넘으면 죽임
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPos);
        if(conquaredDistance > maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        DisableObject();
        var da = collision.GetComponent<Damagable>();
        if (!da) da.Hit(damage);
        //collision.GetComponent<Damagable>().Hit(damage);
    }

    public void Initializes()
    {
        startPos = transform.position;
        rb.velocity = transform.up * speed;
    }

    
}
