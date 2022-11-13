using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels;
    public GameObject bulletPrefab;
    public float reloadDelay = 1;

    private bool canShoot = false;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private ObjectPool bulletPool;
    [SerializeField] private int bulletPoolCount = 10;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        bulletPool.Initialized(bulletPrefab, bulletPoolCount);
    }

    private void Update()
    {
        if(canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            if(currentDelay < 0)
            {
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Debug.Log("Shoooooot!!!");
            canShoot = false;
            currentDelay = reloadDelay;

            foreach(var barrel in turretBarrels)
            {
                //GameObject bullet = Instantiate(bulletPrefab);
                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = barrel.transform.position;
                bullet.transform.rotation = barrel.transform.rotation;
                bullet.GetComponent<Bullet>().Initializes();

                foreach(var col in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), col);
                }
            }
        }
    }
}
